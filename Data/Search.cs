using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using ManageAndStorage.Models;
namespace ManageAndStorage.Data
{
    public class Search
    {
        public IEnumerable<Product> SearchName(ApplicationDbContext Db, string Pattren, int ProductId){
            IQueryable<Product> test = from data in Db.Products where EF.Functions.Like(data.Name,"%"+Pattren+"%") where data.ProductId == ProductId select data;
            

            return test;
        }
        public IEnumerable<Item> SearchNameInSales(ApplicationDbContext Db, string Pattren){
            IQueryable<Item> test = from data in Db.Items where EF.Functions.Like(data.Name,"%"+Pattren+"%") select data;
            

            return test;
        }
        public IEnumerable<Product> SearchId(ApplicationDbContext Db, int Pattren, int ProductId){
            IQueryable<Product> test = from data in Db.Products where EF.Functions.Like(Convert.ToString(data.id), Convert.ToString(Pattren)) where data.ProductId == ProductId select data;
            

            return test;
        }
        public IEnumerable<Item> SearchIdInSales(ApplicationDbContext Db, int Pattren){
            IQueryable<Item> test = from data in Db.Items where EF.Functions.Like(Convert.ToString(data.ItemId), Convert.ToString(Pattren)) select data;
            

            return test;
        }
        public IEnumerable<Product> SearchItemLocation(ApplicationDbContext Db, int Pattren, int ProductId){
            IQueryable<Product> test = from data in Db.Products where EF.Functions.Like(Convert.ToString(data.ItemLocation), Convert.ToString(Pattren)) where data.ProductId == ProductId select data;
            

            return test;
        }
        public IEnumerable<Product> DisplayEmptyItems(ApplicationDbContext Db, int ProductId){
            IQueryable<Product> test = from data in Db.Products where data.AvilableItems == 0 where data.ProductId == ProductId select data;

            return test;
        }
        public IEnumerable<Item> Filtring(ApplicationDbContext Db, string Pattren, string LastDate){
            IQueryable<Item> result = null;

            if(Pattren != ""){
                DateTime CurrentDate = DateTime.Now;
                string year = LastDate[4].ToString() + LastDate[5].ToString() + LastDate[6].ToString() + LastDate[7].ToString();
                DateTime PreviousDate = new DateTime(Int16.Parse(year), Int16.Parse(LastDate[0].ToString()), Int16.Parse(LastDate[2].ToString()));

                if(Pattren == "Last Day"){
                    result = from data in Db.Items where data.SaleDate.Day == PreviousDate.Day select data;
                }
                else if(Pattren == "Last Week"){
                    int DayOfTheLastWeek = (int)PreviousDate.DayOfWeek;
                    int StartOfTheWeek = 0;
                    int EndOfTheWeek = 0;

                   IQueryable<int> Days = (from data in Db.Items where data.SaleDate.Month == PreviousDate.Month || data.SaleDate.Month == PreviousDate.Month-1 select data.SaleDate.Day).Distinct();
                    IList<int> ListOfDays = new List<int>();
                    foreach(var item in Days){
                        ListOfDays.Add(item);
                    }
                    for(int i=ListOfDays.IndexOf(PreviousDate.Day); DayOfTheLastWeek > 0; i--){
                        StartOfTheWeek = ListOfDays[i];
                        DayOfTheLastWeek--;
                        if(i == 0) i = ListOfDays.Count() - 1;
                    }
                    for(int i = ListOfDays.IndexOf(StartOfTheWeek),j=0; j<5; j++,i++){
                        EndOfTheWeek = ListOfDays[i];
                        if(i == ListOfDays.Count()-1) i = -1;
                    }
                    IQueryable<Item> Result = from data in Db.Items where data.SaleDate.Day >= StartOfTheWeek || data.SaleDate.Day >= 1 && data.SaleDate.Day <= EndOfTheWeek select data;

                    result = from data in Db.Items select data;
                    return Result;
                }
            }
            return result;
        }
    }
}
/*0 Sunday -7
1 Monday -6
2 Tuesday -5
3 wensday -4
4 Thursday -3
5 Friday -2
6 Saturday -1 */
/*old code for Last Week
int CurrentDay = CurrentDate.Day;
                    //int CurrentDay = 10;
                    int LastWeekDay = CurrentDay - 7;
                    

                    if(LastWeekDay <= 3){
                        int LastMonth = (int)CurrentDate.Month - 1;
                        IList<Item> Result = new List<Item>();
                        IQueryable<int> Days = (from data in Db.Items where data.SaleDate.Month == LastMonth orderby data.SaleDate.Day descending select data.SaleDate.Day).Distinct();
                        IList<int> ListOfDays = new List<int>();

                        if(LastMonth == 0) LastMonth = 12;
                        

                        foreach(var item in Days){
                            ListOfDays.Add(item);
                        }
                        if(ListOfDays.Count() != 0){
                            switch(LastWeekDay){
                                case 0:
                                case 1:
                                    LastWeekDay = ListOfDays.ElementAt(ListOfDays.Count() - 1);break;
                                case -1:
                                case 2:
                                    LastWeekDay = ListOfDays.ElementAt(ListOfDays.Count() - 2);break;
                                case -2:
                                case 3:
                                    LastWeekDay = ListOfDays.ElementAt(ListOfDays.Count() - 3);break;
                                case -3: LastWeekDay = ListOfDays.ElementAt(ListOfDays.Count() - 4);break;
                                case -4: LastWeekDay = ListOfDays.ElementAt(ListOfDays.Count() - 5);break;
                                case -5: LastWeekDay = ListOfDays.ElementAt(ListOfDays.Count() - 6);break;
                                case -6: LastWeekDay = ListOfDays.ElementAt(ListOfDays.Count() - 7);break;
                            }
                        }else{LastWeekDay = 0;}

                        if(LastWeekDay!= 0){
                            foreach(var item in Db.Items){
                                if(item.SaleDate.Day == LastWeekDay){
                                    if(item.SaleDate.DayOfWeek != 0){
                                        LastWeekDay = item.SaleDate.Day - (int)item.SaleDate.DayOfWeek;
                                    }
                                }
                            }
                        }


                        IQueryable<Item> OldMonth = from data in Db.Items where data.SaleDate.Day >= LastWeekDay where LastWeekDay != 0 select data;
                        int ItemsInOldMonth = OldMonth.Count();
                        IQueryable<Item> CurrentMonth = from data in Db.Items where data.SaleDate.Day <= 5-ItemsInOldMonth where LastWeekDay != 0 select data;
                    
                        int i = 5;
                        foreach(var item in OldMonth){
                            if(i >= 1){
                                Result.Add(item);
                                i--;
                            }
                        }
                        foreach(var item in CurrentMonth){
                            Result.Add(item);
                        }

                        return Result;
                    }
                    //11
                    //else{
                        
                    //}
                    result = from data in Db.Items where data.SaleDate.Day >= LastWeekDay where LastWeekDay != 0 select data;
*/
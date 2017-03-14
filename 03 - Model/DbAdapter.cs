using DataLayer;
using DataLayer.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.MainWnd
{
    public class DbAdapter
    {
        protected EFContext db;

        public DbAdapter(string conString)
        {
            db = new EFContext(conString);
        }

        public List<Place> GetPlaces
        {
            get
            {
                db.Places.Load();
                db.Cars.Load();
                db.Tarriffs.Load();
                db.Clients.Load();
                return db.Places.ToList();
            }
        }
     
        public List<Record> DisplayList
        {
            get
            {
                DisplayRecords dp = new DisplayRecords(db);
                return dp.GetList.ToList();
            }
        }

        public void Add(Place place)
        {
            //номер парковки не может повторятся в базе
            foreach (Place placeCurr in GetPlaces)
            {
                if (place.Number == placeCurr.Number)
                    return;
            }
            db.Places.Add(place);
            try
            {
                db.SaveChanges();
            }
            catch
            {
                //если не все данные будут заполнены
                return;
            }
        }

        public void Remove(Record record)
        {
            foreach (Place place in db.Places)
            {
                if (record.NumberPLace == place.Number)
                {
                    db.Places.Remove(place);
                    break;
                }                  
            }
            db.SaveChanges();
        }

        public void Edit(Place OldPlace, Place NewPlace)
        {           
            foreach (Place placeCurrent in db.Places.Local)
            {
                if (placeCurrent.Number == OldPlace.Number)
                {
                    //delete old data
                    db.Clients.Remove(placeCurrent.Client);
                    db.Cars.Remove(placeCurrent.Car);
                    db.Tarriffs.Remove(placeCurrent.Tarriff);
                    db.Places.Remove(placeCurrent);   
                                    
                    db.Places.Add(NewPlace);                 
                    break;
                }
            }
            try
            {
                db.SaveChanges();
            }
            catch
            {
                //если не все данные будут заполнены
                return;
            }
        }

    }
}

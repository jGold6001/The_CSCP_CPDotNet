using DataLayer;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace _03___Model
{
    public class EFClientOp : EFAdapter, IClientOperations
    {
        public EFClientOp() 
        {
           
        }

        public IDisplayInfo IDisplayInfo { get; set; }
        public IFilter IFilter { get; set; }
        public IPay IPay { get; set; }
        public IRecord IRecord { get; set; }     
        public IAvaPlace ISelectPlace { get; set; }
        public ITarriffEdit ITarriffEdit { get; set; }

        public List<Place> GetPlaces
        {
            get
            {
                db.Places.Load();
                db.Cars.Load();
                db.Tariffs.Load();
                db.Clients.Load();
                return db.Places.ToList();
            }
        }

        public override void Add(object newObj)
        {
            //номер парковки не может повторятся в базе
            Place place = newObj as Place;
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

        public override void Edit(object curObj, object newObj)
        {
            Place oldPlace = curObj as Place;
            Place newPlace = newObj as Place;
            foreach (Place placeCurrent in db.Places.Local)
            {
                if (placeCurrent.Number == oldPlace.Number)
                {
                    //delete old data
                    db.Clients.Remove(placeCurrent.Client);
                    db.Cars.Remove(placeCurrent.Car);
                    db.Tariffs.Remove(placeCurrent.Tariff);
                    db.Places.Remove(placeCurrent);

                    db.Places.Add(newPlace);
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

        public override void Remove(object curObj)
        {
            Record record = curObj as Record;
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

        public void Payment(Tariff tariff, decimal summ)
        {
            IPay = new Pay(tariff);
            Tariff updateTariff = IPay.AddDeposit(summ) as Tariff;

            foreach (var item in db.Tariffs)
            {
                if (item.Id == updateTariff.Id)
                {
                    item.Deposit = updateTariff.Deposit;
                    item.Debt = updateTariff.Debt;
                    item.DatePayment = updateTariff.DatePayment;
                    break;
                }
            }
            db.SaveChanges();
        }
    }
}
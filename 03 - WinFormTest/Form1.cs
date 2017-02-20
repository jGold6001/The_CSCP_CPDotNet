using _04___TestingClasses;
using DataLayer.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ViewModels.MainWnd;
using ViewModels;

namespace _03___WinFormTest
{
    public partial class Form1 : Form
    {
        string home= "name=DataBaseHome";
        string auditory = "name=DataBase";
        DbAdapter dba;
        DbAdapterTest dbat;
        BindingSource bs = new BindingSource();
        AddEditDataFrm aedf;
        BusinessLogic bl;

        BindingSource bsTayrriff = new BindingSource();
        BindingSource bsCar = new BindingSource();
        BindingSource bsClient = new BindingSource();

        public Form1()
        {
            InitializeComponent();          
            dba = new DbAdapter(home);
            dbat = new DbAdapterTest(home);
            bl = new BusinessLogic();

             
            gbRecord.Controls.AddRange(new Control[]{ dgv, btnAdd, btnRemove, btnEdit });          

            dgv.DataSource = bs;
            dgvTarriff.DataSource = bsTayrriff;
            dgvClient.DataSource = bsClient;
            dgvCar.DataSource = bsCar;

            this.UpdateGrids();    
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            aedf = new AddEditDataFrm();
            aedf.ShowDialog();
            if (aedf.DialogResult == DialogResult.OK)           
                dba.Add(this.CreateNewPlace());
            
            this.UpdateGrids();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            Record pex = bs.Current as Record;
            dba.Remove(pex);
            this.UpdateGrids();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            Record rec = bs.Current as Record;
            aedf = new AddEditDataFrm();
            Place oldPlace = null;
            foreach (var item in dba.GetPlaces)
            {
                if (rec.NumberPLace == item.Number)
                {
                    //number
                    aedf.numericNumber.Value = item.Number;

                    //tariff
                    aedf.cbRental.Text = item.Tarriff.Rental;
                    aedf.tbDeposit.Text = item.Tarriff.Deposit.ToString();

                    //client
                    aedf.tbName.Text = item.Client.FirstName;
                    aedf.tbSurname.Text = item.Client.LastName;
                    aedf.tbPassport.Text = item.Client.PassportID;
                    aedf.tbPhone.Text = item.Client.PhoneNumber.ToString();
                    aedf.rtbAditionalInfo.Text = item.Client.AdditionalInfo;

                    //car
                    aedf.tbCarID.Text = item.Car.VehicleID;
                    aedf.cbBrand.Text = item.Car.Brand;
                    aedf.tbVIN.Text = item.Car.VIN;
                    aedf.cbColor.Text = item.Car.Color;

                    oldPlace = item;          
                }
            }

            
            aedf.ShowDialog();
            if (aedf.DialogResult == DialogResult.OK)               
                dba.Edit(oldPlace, this.CreateNewPlace());

            this.UpdateGrids();
        }


        private async void btnLoad_Click(object sender, EventArgs e)
        {
           
            await Task.Factory.StartNew(new Action(() =>
            {
                TestCommand.LoadData(dbat);              
            } ));
            this.UpdateGrids();
        }

        private async void btnClear_Click(object sender, EventArgs e)
        {
            await Task.Factory.StartNew(new Action(() =>
            {
                TestCommand.ClearData(dbat);
            }));
            this.UpdateGrids();
        }


        private void UpdateGrids()
        {
            bs.DataSource = dba.DisplayList;
            bsTayrriff.DataSource = dbat.ListTarriff;
            bsClient.DataSource = dbat.ListClient;
            bsCar.DataSource = dbat.ListCars;
        }

        private Place CreateNewPlace()
        {
            Client client = new Client()
            {
                FirstName = aedf.tbName.Text,
                LastName = aedf.tbSurname.Text,
                PassportID = aedf.tbPassport.Text,
                PhoneNumber = Convert.ToInt64(aedf.tbPhone.Text),
                AdditionalInfo = aedf.rtbAditionalInfo.Text
            };

            Car car = new Car()
            {
                VehicleID = aedf.tbCarID.Text,
                Brand = aedf.cbBrand.Text,
                VIN = aedf.tbVIN.Text,
                Color = aedf.cbColor.Text
            };

            bl.SetCostRental(10, 100);//установка стоимости
            Tarriff tarriff = bl.CreateTarriff(aedf.cbRental.Text, Convert.ToDecimal(aedf.tbDeposit.Text));

            Place place = new Place()
            {
                Number = Convert.ToInt32(aedf.numericNumber.Value),
                Client = client,
                Car = car,
                DateRegistred = DateTime.Now.Date,
                Tarriff = tarriff
            };
            return place;
        }

        private void btnDeposit_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Add Deposit");
        }
    }
}

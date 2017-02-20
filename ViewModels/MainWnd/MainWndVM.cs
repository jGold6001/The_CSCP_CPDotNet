using DataLayer.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.MainWnd
{
    public class MainWndVM
    {
        DbAdapter dbm = new DbAdapter("name= DataBaseHome");

        //DataGrid
        public List<Record> GetData
        {
            get{
                return dbm.DisplayList;
            }          
        }


        //add
        private TemplateCommand addCommand;
        public TemplateCommand AddCommand
        {
            get
            {
                return addCommand ??
                    (addCommand = new TemplateCommand(obj =>
                    {
                        System.Windows.Forms.MessageBox.Show("AddCommand");
                    }));
            }
         
        }


        //remove
        private TemplateCommand removeCommand;
        public TemplateCommand RemoveCommand
        {
            get
            {
                return removeCommand ??
                    (removeCommand = new TemplateCommand(obj =>
                    {
                        System.Windows.Forms.MessageBox.Show("RemoveCommand");
                    }));
            }

        }


        //edit
        private TemplateCommand editCommand;
        public TemplateCommand EditCommand
        {
            get
            {
                return editCommand ??
                    (editCommand = new TemplateCommand(obj =>
                    {
                        System.Windows.Forms.MessageBox.Show("EditCommand");
                    }));
            }

        }


        //filter
        private TemplateCommand filterCommand;
        public TemplateCommand FilterCommand
        {
            get
            {
                return filterCommand ??
                    (filterCommand = new TemplateCommand(obj =>
                    {
                        System.Windows.Forms.MessageBox.Show("FilterCommand");
                    }));
            }

        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ValueButtonLib
{
    public partial class ValueButton: Button
    {

        // Creates the private variable that will store the value of your
        // property.
        private int varValue;
        // Declares the property.
        public int ButtonValue
        {
            // Sets the method for retrieving the value of your property.
            get
            {
                return varValue;
            }
            // Sets the method for setting the value of your property.
            set
            {
                varValue = value;
            }
        }

        public ValueButton()
        {
            InitializeComponent();
        }
    }
}

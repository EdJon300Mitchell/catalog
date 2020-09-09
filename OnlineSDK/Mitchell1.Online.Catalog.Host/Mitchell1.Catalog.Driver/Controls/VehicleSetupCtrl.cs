using System;
using System.Globalization;
using System.Windows.Forms;
using Mitchell1.Catalog.Driver.Helpers;

namespace Mitchell1.Catalog.Driver.Controls
{
	public partial class VehicleSetupCtrl : UserControl
	{ 
		private readonly Vehicle vehicle;
		private readonly CheckBoxWithText year;
		private readonly CheckBoxWithText vin;
		private readonly CheckBoxWithText transmission;
		private readonly CheckBoxWithText subModel;
		private readonly CheckBoxWithText model;
		private readonly CheckBoxWithText make;
		private readonly CheckBoxWithText gvw;
		private readonly CheckBoxWithText engine;
		private readonly CheckBoxWithText driveType;
		private readonly CheckBoxWithText acesBaseId;
		private readonly CheckBoxWithText brakes;
		private readonly CheckBoxWithText body;
		private readonly CheckBoxWithText aces;
		private readonly CheckBoxWithText aaia;
	    private readonly CheckBoxWithText acesEngineConfigId;
	    private readonly CheckBoxWithText acesSubModelId;
	    private readonly CheckBoxWithText acesEngineId;

		internal VehicleSetupCtrl(Vehicle vehicle)
		{
			this.vehicle = vehicle;
            InitializeComponent();
			year = new CheckBoxWithText(checkBoxYear, textBoxYear, vehicle.Year.ToString());
			vin = new CheckBoxWithText(checkBoxVIN, textBoxVIN, vehicle.Vin);
			transmission = new CheckBoxWithText(checkBoxTransmission, textBoxTransmission, vehicle.Transmission);
			subModel = new CheckBoxWithText(checkBoxSubModel, textBoxSubModel, vehicle.SubModel);
			model = new CheckBoxWithText(checkBoxModel, textBoxModel, vehicle.Model);
			make = new CheckBoxWithText(checkBoxMake, textBoxMake, vehicle.Make);
			gvw = new CheckBoxWithText(checkBoxGVW, textBoxGVW, vehicle.Gvw);
			engine = new CheckBoxWithText(checkBoxEngine, textBoxEngine, vehicle.Engine);
			driveType = new CheckBoxWithText(checkBoxDriveType, textBoxDriveType, vehicle.DriveType);
			acesBaseId = new CheckBoxWithText(checkBoxACESBaseId, textBoxACESBaseId, vehicle.AcesBaseId.ToString());
			brakes = new CheckBoxWithText(checkBoxBrakes, textBoxBrakes, vehicle.Brake);
			body = new CheckBoxWithText(checkBoxBody, textBoxBody, vehicle.Body);
			aces = new CheckBoxWithText(checkBoxACES, textBoxACES, vehicle.AcesId.ToString(CultureInfo.InvariantCulture));
			aaia = new CheckBoxWithText(checkBoxAAIA, textBoxAAIA, vehicle.AaiaId.ToString(CultureInfo.InvariantCulture));
            acesBaseId = new CheckBoxWithText(checkBoxACESBaseId, textBoxACESBaseId, vehicle.AcesBaseId.ToString(CultureInfo.InvariantCulture));
            acesEngineConfigId = new CheckBoxWithText(checkBoxACESEngineConfigID, textBoxACESEngineCfgId, vehicle.AcesEngineConfigId.ToString(CultureInfo.InvariantCulture));
            acesSubModelId = new CheckBoxWithText(checkBoxACESSubModelID, textBoxACESSubModelId, vehicle.AcesSubModelId.ToString(CultureInfo.InvariantCulture));
            acesEngineId = new CheckBoxWithText(checkBoxACESEngineId, textBoxACESEngineid, vehicle.AcesEngineId.ToString(CultureInfo.InvariantCulture));
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
			vehicle.AaiaId = aaia.Checked ? aaia.ToInt32() : 0;
        	vehicle.AcesId = aces.Checked ? aces.ToInt32() : 0;
        	vehicle.AcesBaseId = acesBaseId.Checked ? acesBaseId.ToInt32() : 0;
            vehicle.AcesEngineConfigId = acesEngineConfigId.Checked ? acesEngineConfigId.ToInt32() : 0;
            vehicle.AcesSubModelId = acesSubModelId.Checked ? acesSubModelId.ToInt32() : 0;
            vehicle.AcesEngineId = acesEngineId.Checked ? acesEngineId.ToInt32() : 0;
            vehicle.Body = body.CheckedText;
    		vehicle.Brake = brakes.CheckedText;
    		vehicle.DriveType = driveType.CheckedText;
    		vehicle.Engine = engine.CheckedText;
    		vehicle.Gvw = gvw.CheckedText;
    		vehicle.Make = make.CheckedText;
    		vehicle.Model = model.CheckedText;
    		vehicle.SubModel = subModel.CheckedText;
    		vehicle.Transmission = transmission.CheckedText;
    		vehicle.Vin = vin.CheckedText;
			vehicle.Year = year.Checked ? year.ToInt32() : 0;

    		vehicle.Qualifier = string.Empty;

    		MessageBox.Show("Vehicle has been set to this configuration.");
        }

		private void comboBoxVehicle_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (comboBoxVehicle.Text == "2000 Chrysler Town & Country")
			{
				textBoxYear.Text = "2000";
				textBoxVIN.Text = "1C4GP64LXYB608478";
				textBoxTransmission.Text = "4 speed Automatic 41AE";
				textBoxSubModel.Text = "Limited";
				textBoxModel.Text = "Town & Country";
				textBoxMake.Text = "Chrysler";
				textBoxGVW.Text = "4650";
				textBoxEngine.Text = "3.3L,V6 (201CI) VIN(R)";
				textBoxDriveType.Text = "FWD";
				textBoxBrakes.Text = "4-Wheel ABS";
				textBoxBody.Text = "Van Passenger";
				textBoxACES.Text = "2616";
				textBoxAAIA.Text = "1361674";
			    textBoxACESBaseId.Text = "1576";
			    textBoxACESEngineid.Text = "251";
			    textBoxACESEngineCfgId.Text = "639";
                textBoxACESSubModelId.Text = "13";
            }
			else if (comboBoxVehicle.Text == "1991 Ford LTD Crown Victoria Country Squire")
			{
				textBoxYear.Text = "1991";
				textBoxVIN.Text = "2FACP79F1MX148809";
				textBoxTransmission.Text = "4 speed Automatic AOD";
				textBoxSubModel.Text = "Country Squire LX";
				textBoxModel.Text = "LTD Crown Victoria";
				textBoxMake.Text = "Ford";
				textBoxGVW.Text = "4100";
				textBoxEngine.Text = "5.0L,V8 (302CI) VIN(F)";
				textBoxDriveType.Text = "RWD";
				textBoxBrakes.Text = "Not Available";
				textBoxBody.Text = "4D Wagon";
				textBoxACES.Text = "11164";
				textBoxAAIA.Text = "1107605";
                textBoxACESBaseId.Text = "4989";
                textBoxACESEngineid.Text = "143";
                textBoxACESEngineCfgId.Text = "3564";
                textBoxACESSubModelId.Text = "113";
            }
			else if (comboBoxVehicle.Text == "2004 Dodge Cab & Chassis R3500")
			{
				textBoxYear.Text = "2004";
				textBoxVIN.Text = "3D7LU38694G154760";
				textBoxTransmission.Text = "5 speed Standard NV4500";
				textBoxSubModel.Text = "R3500";
				textBoxModel.Text = "ST";
				textBoxMake.Text = "Dodge";
				textBoxGVW.Text = "9900-11500";
				textBoxEngine.Text = "5.9L,In-Line6 (360CI) VIN(6)";
				textBoxDriveType.Text = "4WD";
				textBoxBrakes.Text = "4-Wheel ABS";
				textBoxBody.Text = "4D Pickup";
				textBoxACES.Text = "54410";
				textBoxAAIA.Text = "1424685";
                textBoxACESBaseId.Text = "17863";
                textBoxACESEngineid.Text = "1126";
                textBoxACESEngineCfgId.Text = "8883";
                textBoxACESSubModelId.Text = "973";
            }
			else if (comboBoxVehicle.Text == "1999 Cadillac DeVille d'Elegance")
			{
				textBoxYear.Text = "1999";
				textBoxVIN.Text = "1G6KE54Y3XU773011";
				textBoxTransmission.Text = "4 speed Automatic 4T80E/MH1";
				textBoxSubModel.Text = "d'Elegance";
				textBoxModel.Text = "DeVille";
				textBoxMake.Text = "Cadillac";
				textBoxGVW.Text = "4100";
				textBoxEngine.Text = "4.6L,V8 (279CI) VIN(Y)";
				textBoxDriveType.Text = "FWD";
				textBoxBrakes.Text = "4-Wheel ABS";
				textBoxBody.Text = "4D Sedan";
				textBoxACES.Text = "5233";
				textBoxAAIA.Text = "1352876";
                textBoxACESBaseId.Text = "2799";
                textBoxACESEngineid.Text = "388";
                textBoxACESEngineCfgId.Text = "1278";
                textBoxACESSubModelId.Text = "405";
            }
		}

		private void TextBox_TextChanged(object sender, EventArgs e)
		{
			CheckBoxWithText checkBoxWithText = (CheckBoxWithText)((TextBox)sender).Tag;
			checkBoxWithText.Checked = checkBoxWithText.Text.Length > 0;
		}

		private void MaskedTextBox_TextChanged(object sender, EventArgs e)
		{
			CheckBoxWithText checkBoxWithText = (CheckBoxWithText)((MaskedTextBox)sender).Tag;
			checkBoxWithText.Checked = (checkBoxWithText.Text.Length > 0 && checkBoxWithText.Text != "0");
		}

		private class CheckBoxWithText
		{
			private readonly CheckBox checkBox;
			private readonly TextBox textBox;
			private readonly MaskedTextBox maskedTextBox;

			public CheckBoxWithText(CheckBox checkBox, TextBox textBox, string text)

			{
				this.textBox = textBox;
				textBox.Tag = this;
				maskedTextBox = null;
				this.checkBox = checkBox;
				Text = text;
			}

			public CheckBoxWithText(CheckBox checkBox, MaskedTextBox maskedTextBox, string text)
			{
				this.maskedTextBox = maskedTextBox;
				maskedTextBox.Tag = this;
				textBox = null;
				this.checkBox = checkBox;
				Text = text;
			}

			public string Text
			{
				get
				{
					return textBox != null ? textBox.Text : maskedTextBox.Text;
				}
				set
				{
					if (textBox != null)
					{
						textBox.Text = value;
					}
					else
					{
						maskedTextBox.Text = value;
					}
				}
			}

			public bool Checked
			{
				get { return checkBox.Checked; }
				set { checkBox.Checked = value; }
			}

			public string CheckedText
			{
				get { return Checked ? Text : string.Empty; }
			}

			public int ToInt32()
			{
				int result;
				if (Checked)
				{
					Int32.TryParse(Text, out result);
					Text = result.ToString();
				}
				else
				{
					result = 0;
				}
				return result;
			}
		}
    }
}

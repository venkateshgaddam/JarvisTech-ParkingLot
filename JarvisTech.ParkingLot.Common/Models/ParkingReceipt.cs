using System;
using System.Collections.Generic;
using System.Text;

namespace JarvisTech.ParkingLot.Common.Models
{
    public class ParkingReceipt
    {
        private string receiptNumber;

        public string ReceiptNumber
        {
            get
            {
                return receiptNumber;
            }
            set
            {
                receiptNumber = value;
            }
        }


        private DateTime entryTime;

        public DateTime EntryTime
        {
            get
            {
                return entryTime;
            }
            set
            {
                entryTime = value;
            }
        }

        private DateTime exitTime;

        public DateTime ExitTime
        {
            get
            {
                return exitTime;
            }
            set
            {
                exitTime = value;
            }
        }

        private double fee;

        public double Fee
        {
            get
            {
                return fee;
            }
            set
            {
                fee = value;
            }
        }
    }
}

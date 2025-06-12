using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATA_ACCESS.Exten.SqlBulkLoader
{
    public class DataReader : ObjectDataReader
    {
        private object[] _values;


        private StreamReader _stream;

        public DataReader(string fileName)
        {
            _stream = new StreamReader(fileName);
            _values = new object[FieldCount];
        }

        public override int FieldCount
        {
            get
            {
                return GetColumns();
            }
        }

        protected bool Eof { get; set; }

        protected string CurrentRecord { get; set; }

        protected int CurrentIndex { get; set; }

        protected int count { get; set; }

        public override object this[int i]
        {
            get { return _values[i]; }
        }

        public override bool NextResult()
        {
            return false;
        }

        public override void Close()
        {
            Array.Clear(_values, 0, _values.Length);
            _stream.Close();
            _stream.Dispose();
        }

        public override bool Read()
        {
            try
            {
                CurrentIndex++;
                CurrentRecord = _stream.ReadLine();
                Eof = String.IsNullOrEmpty(CurrentRecord);
                if (Eof && !_stream.EndOfStream)
                {
                    do
                    {
                        NextResult();
                        CurrentRecord = _stream.ReadLine();
                        Eof = String.IsNullOrEmpty(CurrentRecord);
                        ////For Empty files with lines
                        if (_stream.EndOfStream)
                        {
                            break;
                        }
                    }
                    while (Eof);
                }

                if (!Eof)
                {
                    Fill(_values);
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                throw;
            }


            return !Eof;
        }

        public override object GetValue(int i)
        {
            return _values[i];
        }

        public override void Dispose()
        {
            _stream.Close();
            _stream.Dispose();
        }

        //private void Fill(object[] values)
        //{
        //    values[0] = CurrentRecord.Split(',')[0];
        //    values[1] = CurrentRecord.Split(',')[1];
        //}

        private void Fill(object[] values)
        {

            try
            {
                count = count + 1;
                values[0] = CurrentRecord.Split(',')[0];
                values[1] = CurrentRecord.Split(',')[1];
                values[2] = CurrentRecord.Split(',')[2];
                values[3] = CurrentRecord.Split(',')[3];
                values[4] = CurrentRecord.Split(',')[4];
                values[5] = Convert.ToDateTime(DateTime.Now);
                values[6] = CurrentRecord.Split(',')[6];
                values[7] = Convert.ToDateTime(DateTime.Now);
                values[8] = CurrentRecord.Split(',')[8];
                values[9] = CurrentRecord.Split(',')[9];
                values[10] = CurrentRecord.Split(',')[10];
                values[11] = CurrentRecord.Split(',')[11];
                values[12] = CurrentRecord.Split(',')[12];
                values[13] = Convert.ToDecimal(1);
                values[14] = CurrentRecord.Split(',')[14];
                values[15] = CurrentRecord.Split(',')[15];
                values[16] = CurrentRecord.Split(',')[16];
                values[17] = CurrentRecord.Split(',')[17];
                values[18] = CurrentRecord.Split(',')[18];
                values[19] = CurrentRecord.Split(',')[19];
                values[20] = CurrentRecord.Split(',')[20];
                values[21] = CurrentRecord.Split(',')[21];
                values[22] = Convert.ToDateTime(DateTime.Now);
                values[23] = CurrentRecord.Split(',')[23];
                values[24] = CurrentRecord.Split(',')[24];
                values[25] = Convert.ToDecimal(1);
                values[26] = Convert.ToDecimal(1);
                values[27] = CurrentRecord.Split(',')[27];
                values[28] = CurrentRecord.Split(',')[28];
                values[29] = CurrentRecord.Split(',')[29];
                values[30] = CurrentRecord.Split(',')[30];
                values[31] = CurrentRecord.Split(',')[31];
                values[32] = CurrentRecord.Split(',')[32];
                values[33] = CurrentRecord.Split(',')[33];
                values[34] = CurrentRecord.Split(',')[34];
                values[35] = CurrentRecord.Split(',')[35];
                values[36] = CurrentRecord.Split(',')[36];
                values[37] = CurrentRecord.Split(',')[37];
                values[38] = CurrentRecord.Split(',')[38];
                values[39] = CurrentRecord.Split(',')[39];
                values[40] = CurrentRecord.Split(',')[40];
                values[41] = CurrentRecord.Split(',')[41];
                values[42] = CurrentRecord.Split(',')[42];
                values[43] = CurrentRecord.Split(',')[43];
                values[44] = CurrentRecord.Split(',')[44];
                values[45] = CurrentRecord.Split(',')[45];
                values[46] = CurrentRecord.Split(',')[46];
                values[47] = CurrentRecord.Split(',')[47];
                values[48] = CurrentRecord.Split(',')[48];
                values[49] = CurrentRecord.Split(',')[49];
                values[50] = CurrentRecord.Split(',')[50];
                values[51] = CurrentRecord.Split(',')[51];
                values[52] = CurrentRecord.Split(',')[52];
                values[53] = CurrentRecord.Split(',')[53];
                values[54] = CurrentRecord.Split(',')[54];
                values[55] = CurrentRecord.Split(',')[55];
                values[56] = CurrentRecord.Split(',')[56];
                values[57] = CurrentRecord.Split(',')[57];
                values[58] = CurrentRecord.Split(',')[58];
                values[59] = CurrentRecord.Split(',')[59];
                values[60] = CurrentRecord.Split(',')[60];
                values[61] = CurrentRecord.Split(',')[61];
                values[62] = CurrentRecord.Split(',')[62];
                values[63] = CurrentRecord.Split(',')[63];
                values[64] = CurrentRecord.Split(',')[64];
                values[65] = CurrentRecord.Split(',')[65];
                values[66] = CurrentRecord.Split(',')[66];
                values[67] = CurrentRecord.Split(',')[67];
                values[68] = CurrentRecord.Split(',')[68];
                values[69] = Convert.ToDateTime(DateTime.Now);

            }
            catch (Exception ex)
            {
                String mensaje = ex.Message + "linea" + count;

                throw;
            }

        }

        private int GetColumns()
        {
            return 70;
        }

    }

}

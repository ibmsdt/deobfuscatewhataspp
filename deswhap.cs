using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;

namespace deswhap
{
    class Program
    {
        public static string descifra(string cadena, int a, int b, int c, int d, int e)
        {
           
            char[] var0 = cadena.ToCharArray();
            char[] var1 = cadena.ToCharArray();
            int flag = 0;
            int j = 0;
            int i = 0;
            for (j = 0; j < var0.Length; j++)
            {
                if ((var0[j] == '\\'))
                    j++;
                var1[i] = var0[j];
                i++;

            }

            int var2 = var1.Length;

            for (int var3 = 0; var2 > var3; ++var3)
            {
                char var4 = var1[var3];
                byte var5;
                switch (var3 % 5)
                {
                    case 0:
                        var5 = (byte)a;
                        break;
                    case 1:
                        var5 = (byte)b;
                        break;
                    case 2:
                        var5 = (byte)c;
                        break;
                    case 3:
                        var5 = (byte)d;
                        break;
                    default:
                        var5 = (byte)e;
                        break;
                }

                var1[var3] = (char)(var5 ^ var4);
            }
            string h = "";
            foreach (char letra in var1)
                h = h + letra;
            return h;
        }
        static void Main(string[] args)
        {

            string[] fileList = Directory.GetFiles(@"D:\w289\2salida\");

            foreach (string ruta in fileList)
            {
                string rutaw = ruta.Replace(@"D:\w289\2salida\", "");
                string pathw = @"D:\salidanuevaversion\" + rutaw;
                string path = ruta;
                using (StreamReader sr = new StreamReader(path))
                {
                    using (StreamWriter sw = new StreamWriter(pathw))
                    {
                        int indice = 0;
                        string cadena = "";
                        int a = 0;
                        int b = 0;
                        int c = 0;
                        int d = 0;
                        int e = 0;
                        int pos = 0;
                        string num = "";
                        int lo = 0;
                        string escribir = "";
                        ArrayList resultado = new ArrayList();
                        while (sr.Peek() >= 0)
                        {
                            string linea = sr.ReadLine();
                            if ((linea.Contains("public")) || linea.Contains("private"))
                            {
                                indice = 3;
                                sw.WriteLine(linea);
                            }
                            else
                                if ((indice == 3) && linea.Contains("(z)"))
                                {
                                    escribir = linea.Replace("(z)", "(" + resultado[0] + ")");
                                    sw.WriteLine(escribir);
                                }
                                else
                                    if ((indice == 3) && linea.Contains("z["))
                                    {
                                        try
                                        {
                                            lo = linea.IndexOf("]", linea.IndexOf("z[")) - linea.IndexOf("z[") - 2;
                                            num = linea.Substring(linea.IndexOf("z[") + 2, lo);
                                            a = Convert.ToInt32(num);
                                            escribir = linea.Replace("z[" + a.ToString() + "]", (string)resultado[a]);
                                            sw.WriteLine(escribir);
                                        }
                                        catch (Exception ex)
                                        { }
                                    }
                                    else
                                        if (indice == 3)
                                        {
                                            sw.WriteLine(linea);
                                        }


                            if (linea.Contains("(new String"))
                            {
                                resultado.Add(descifra(cadena, a, b, c, d, e));
                                indice = 1;
                            }

                            if (linea.Contains("static {"))
                            {
                                indice = 1;
                            }
                            if ((indice == 1) && (linea.Contains("char[] var")) && (linea.Contains("\"")))
                            {
                                int t = linea.IndexOf("\".toC") - linea.IndexOf("= \"");
                                cadena = linea.Substring(linea.IndexOf("= \"") + 3, t - 3);

                            }
                            if (linea.Contains("switch(") && linea.Contains("%"))
                            {
                                indice = 2;

                            }
                            if ((indice == 2) && linea.Contains("= "))
                            {
                                if (pos == 0)
                                {
                                    lo = linea.IndexOf(";") - linea.IndexOf("= ") - 2;
                                    num = linea.Substring(linea.IndexOf("= ") + 2, lo);
                                    a = Convert.ToInt32(num);
                                }
                                if (pos == 1)
                                {
                                    lo = linea.IndexOf(";") - linea.IndexOf("= ") - 2;
                                    num = linea.Substring(linea.IndexOf("= ") + 2, lo);
                                    b = Convert.ToInt32(num);


                                }
                                if (pos == 2)
                                {
                                    lo = linea.IndexOf(";") - linea.IndexOf("= ") - 2;
                                    num = linea.Substring(linea.IndexOf("= ") + 2, lo);
                                    c = Convert.ToInt32(num);


                                }
                                if (pos == 3)
                                {
                                    lo = linea.IndexOf(";") - linea.IndexOf("= ") - 2;
                                    num = linea.Substring(linea.IndexOf("= ") + 2, lo);
                                    d = Convert.ToInt32(num);


                                }
                                if (pos == 4)
                                {
                                    lo = linea.IndexOf(";") - linea.IndexOf("= ") - 2;
                                    num = linea.Substring(linea.IndexOf("= ") + 2, lo);
                                    e = Convert.ToInt32(num);


                                }
                                pos++;
                            }
                        }

                    }
                }

            }
        }

    }
}

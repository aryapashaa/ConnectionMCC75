using ConnectionMCC75.Command;
using ConnectionMCC75.Models;
using System.Data.SqlClient;
using System.Security.Cryptography.X509Certificates;

namespace ConnectionMCC75;

public class Program
{
    public static Region region = new Region();
    public static RegionCommands command_region = new RegionCommands();

    public static Country country = new Country();
    public static CountryCommands command_country = new CountryCommands();

    public static void Main()
    {
        Console.Clear();
        Console.WriteLine("======CRUD======");
        string[] table = new string[] { "Regions", "Countries" , "Exit"};
        for (int i = 0; i < table.Length; i++)
        {
            Console.WriteLine($"[{i + 1}] - {table[i]}");
        }
        Console.WriteLine("================");
        Console.Write("Pilih Tabel: ");
        int pilihTabel = Convert.ToInt32(Console.ReadLine());

        switch (pilihTabel)
        {
            case 1:
                TableRegions();
                break;
            case 2:
                TableCountries();
                break;
            case 3:
                System.Environment.Exit(0);
                break;
            default:
                break;
        }
    }
    public static void TableRegions()
    {
        bool key = true;
        do
        {
            Console.Clear();
            Console.WriteLine("==REGIONS TABLE==");
            Console.WriteLine("\n=======MENU======");
            string[] crud = new string[] { "CREATE", "READ", "READ BY ID", "UPDATE", "DELETE", "BACK" };
            for (int i = 0; i < crud.Length; i++)
            {
                Console.WriteLine($"[{i + 1}] - {crud[i]}");
            }
            Console.WriteLine("=================");
            Console.Write("Pilih Menu: ");
            int pilihMenu = Convert.ToInt32(Console.ReadLine());

            switch (pilihMenu)
            {
                case 1: //Create
                    Console.Clear();
                    Console.Write("Masukkan nama Region baru : ");
                    string inputRegion = Console.ReadLine();
                    region.Name = inputRegion;
                    int resultCreate = command_region.Insert(region);
                    if (resultCreate > 0)
                    {
                        Console.WriteLine("Data Berhasil Disimpan");
                    }
                    else
                    {
                        Console.WriteLine("Data Gagal Disimpan");
                    }
                    Console.ReadKey();
                    break;
                case 2: //Read
                    Console.Clear();
                    List<Region> resultRead = command_region.GetAll();
                    if (resultRead == null)
                    {
                        Console.WriteLine("Data tidak ditemukan");
                    }
                    else
                    {
                        foreach (var item in resultRead)
                        {
                            Console.WriteLine("Id : " + item.Id);
                            Console.WriteLine("Name : " + item.Name);
                        }
                    }
                    Console.ReadKey();
                    break;
                case 3:
                    Console.Clear();
                    Console.Write("Masukkan ID yang ingin ditampilkan : ");
                    int tampilId = Convert.ToInt32(Console.ReadLine());
                    Region resultReadById = command_region.GetById(tampilId);
                    if (resultReadById == null)
                    {
                        Console.WriteLine("Data tidak ditemukan");
                    }
                    else
                    {
                        Console.WriteLine("Id : " + resultReadById.Id);
                        Console.WriteLine("Name : " + resultReadById.Name);
                    }
                    Console.ReadKey();
                    break;
                case 4:
                    Console.Clear();
                    Console.Write("Masukkan ID yang ingin diubah : ");
                    int editId = Convert.ToInt32(Console.ReadLine());
                    Console.Write("Masukkan nama Region baru : ");
                    string editRegion = Console.ReadLine();
                    region.Id = editId;
                    region.Name = editRegion;
                    int resultUpdate = command_region.Update(region);
                    if (resultUpdate > 0)
                    {
                        Console.WriteLine("Data Berhasil Diperbaharui");
                    }
                    else
                    {
                        Console.WriteLine("Data Gagal Diperbaharui");
                    }
                    Console.ReadKey();
                    break;
                case 5:
                    Console.Clear();
                    Console.Write("Masukkan ID yang ingin dihapus : ");
                    int hapusId = Convert.ToInt32(Console.ReadLine());
                    int resultDelete = command_region.Delete(hapusId);
                    if (resultDelete > 0)
                    {
                        Console.WriteLine("Data Berhasil Dihapus");
                    }
                    else
                    {
                        Console.WriteLine("Data Gagal Dihapus");
                    }
                    Console.ReadKey();
                    break;
                case 6:
                    Main();
                    break;
                default:
                    Console.Write("Pilihan salah");
                    Console.ReadKey();
                    break;
            }
        } while (key);
    }
    public static void TableCountries()
    {
        bool key = true;
        do
        {
            Console.Clear();
            Console.WriteLine("==COUNTRIES TABLE==");
            Console.WriteLine("\n========MENU=======");
            string[] crud = new string[] { "CREATE", "READ", "READ BY ID", "UPDATE", "DELETE", "BACK" };
            for (int i = 0; i < crud.Length; i++)
            {
                Console.WriteLine($"[{i + 1}] - {crud[i]}");
            }
            Console.WriteLine("===================");
            Console.Write("Pilih Menu: ");
            int pilihMenu = Convert.ToInt32(Console.ReadLine());

            switch (pilihMenu)
            {
                case 1: //Create
                    Console.Clear();
                    Console.Write("Masukkan nama Country baru : ");
                    string inputCountry = Console.ReadLine();
                    Console.Write("Masukkan Region ID-nya : ");
                    int inputRegId = Convert.ToInt32(Console.ReadLine());
                    country.Name = inputCountry;
                    country.RegionId = inputRegId;
                    int resultCreate = command_country.Insert(country);
                    if (resultCreate > 0)
                    {
                        Console.WriteLine("Data Berhasil Disimpan");
                    }
                    else
                    {
                        Console.WriteLine("Data Gagal Disimpan");
                    }
                    Console.ReadKey();
                    break;
                case 2: //Read
                    Console.Clear();
                    List<Country> resultRead = command_country.GetAll();
                    if (resultRead == null)
                    {
                        Console.WriteLine("Data tidak ditemukan");
                    }
                    else
                    {
                        foreach (var item in resultRead)
                        {
                            Console.WriteLine("Id : " + item.Id);
                            Console.WriteLine("Name : " + item.Name);
                            Console.WriteLine("Region Id : " + item.RegionId);
                        }
                    }
                    Console.ReadKey();
                    break;
                case 3:
                    Console.Clear();
                    Console.Write("Masukkan ID yang ingin ditampilkan : ");
                    int tampilId = Convert.ToInt32(Console.ReadLine());
                    Country resultReadById = command_country.GetById(tampilId);
                    if (resultReadById == null)
                    {
                        Console.WriteLine("Data tidak ditemukan");
                    }
                    else
                    {
                        Console.WriteLine("Id : " + resultReadById.Id);
                        Console.WriteLine("Name : " + resultReadById.Name);
                        Console.WriteLine("Region Id : " + resultReadById.RegionId);
                    }
                    Console.ReadKey();
                    break;
                case 4:
                    Console.Clear();
                    Console.Write("Masukkan ID yang ingin diubah : ");
                    int editId = Convert.ToInt32(Console.ReadLine());
                    Console.Write("Masukkan nama Country baru : ");
                    string editCountry = Console.ReadLine();
                    country.Id = editId;
                    country.Name = editCountry;
                    int resultUpdate = command_country.Update(country);
                    if (resultUpdate > 0)
                    {
                        Console.WriteLine("Data Berhasil Diperbaharui");
                    }
                    else
                    {
                        Console.WriteLine("Data Gagal Diperbaharui");
                    }
                    Console.ReadKey();
                    break;
                case 5:
                    Console.Clear();
                    Console.Write("Masukkan ID yang ingin dihapus : ");
                    int hapusId = Convert.ToInt32(Console.ReadLine());
                    int resultDelete = command_country.Delete(hapusId);
                    if (resultDelete > 0)
                    {
                        Console.WriteLine("Data Berhasil Dihapus");
                    }
                    else
                    {
                        Console.WriteLine("Data Gagal Dihapus");
                    }
                    Console.ReadKey();
                    break;
                case 6:
                    Main();
                    break;
                default:
                    Console.Write("Pilihan salah");
                    Console.ReadKey();
                    break;
            }
        } while (key);
    }

}
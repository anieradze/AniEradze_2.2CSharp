using System;
using System.ComponentModel;

public class Personal
{
    public int PersonaliID { get; set; }

    [DisplayName("გვარი")]
    public string Gvari { get; set; }

    [DisplayName("სახელი")]
    public string Saxeli { get; set; }

    [DisplayName("განყოფილება")]
    public string Ganyofileba { get; set; }

    [DisplayName("ქალაქი")]
    public string Qalaqi { get; set; }

    [DisplayName("რეგიონი")]
    public string Regioni { get; set; }

    [DisplayName("რაიონი")]
    public string Raioni { get; set; }

    [DisplayName("ხელფასი")]
    public decimal Xelfasi { get; set; }

    [DisplayName("ასაკი")]
    public int Asaki { get; set; }

    [DisplayName("სტაჟი")]
    public int Staji { get; set; }

    [DisplayName("დაბადების თარიღი")]
    public DateTime TarigiDabadebis { get; set; }

    [DisplayName("სქესი")]
    public string Sqesi { get; set; }

    [DisplayName("მისამართი")]
    public string MisamartiSaxlis { get; set; }

    [DisplayName("სახლი ტელეფონი")]
    public string TeleponiSaxlis { get; set; }

    [DisplayName("მობილური")]
    public string Mobiluri { get; set; }

    [DisplayName("ელ.ფოსტა")]
    public string Email { get; set; }

    [DisplayName("იერარქია")]
    public string Ierarqia { get; set; }
}

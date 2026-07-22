namespace M08_Templates;

//Bsp-Klasse
public class Person
{
    public string Vorname { get; set; }
    public string Nachname { get; set; }
    public int Alter { get; set; }

    public override string ToString()
    {
        return $"Person: {Nachname}";
    }
}
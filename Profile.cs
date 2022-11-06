//COLE ELFSTROM

public class Profile
{
    public string name;
    public string phoneNumber;
    public string emailAddress;
    public string address;

    public Profile(string n, string p, string e, string a)
    {
        this.name = n;
        this.phoneNumber = p;
        this.emailAddress = e;
        this.address = a;
    }

    public void setName(string n)
    {
        name = n;
    }

    public void setPhoneNumber(string p)
    {
        phoneNumber = p;
    }
}
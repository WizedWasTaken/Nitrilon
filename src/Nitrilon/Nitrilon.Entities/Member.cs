namespace Nitrilon.Entities;

public class Member
{
    #region Fields
    private int memberId;
    private string name;
    private string phoneNumber;
    private string email;
    private DateTime enrollmentDate;
    private Membership membership;
    #endregion

    #region Constructors

    public Member()
    {
    }

    public Member(int memberId, string name, string phoneNumber, string email)
    {
        MemberId = memberId;
        Name = name;
        PhoneNumber = phoneNumber;
        Email = email;
        enrollmentDate = DateTime.Now;
    }

    public Member(int memberId, string name, string phoneNumber, string email, DateTime enrollmentDate,
        Membership membership)
    {
        MemberId = memberId;
        Name = name;
        PhoneNumber = phoneNumber;
        Email = email;
        EnrollmentDate = enrollmentDate;
        Membership = membership;
    }
    #endregion

    #region Properties
    public int MemberId
    {
        get => memberId; set => memberId = value;
    }

    public string Name
    {
        get => name; set => name = value;
    }

    public string PhoneNumber
    {
        get => phoneNumber; set => phoneNumber = value;
    }

    public string Email
    {
        get => email; set => email = value;
    }

    public DateTime EnrollmentDate
    {
        get => enrollmentDate; set => enrollmentDate = value;
    }

    public Membership Membership
    {
        get => membership; set => membership = value;
    }
    #endregion


}
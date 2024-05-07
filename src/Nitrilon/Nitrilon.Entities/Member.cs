using System.Text.Json.Serialization;

namespace Nitrilon.Entities;

public class Member
{
    #region Fields
    private int memberId;
    private string name;
    private string phoneNumber;
    private string email;
    private DateTime enrollmentDate;
    private bool isDeleted;
    private Membership membership;
    #endregion

    #region Constructors

    public Member(int memberId, string name, string phoneNumber, string email, bool isDeleted)
    {
        MemberId = memberId;
        Name = name;
        PhoneNumber = phoneNumber;
        Email = email;
        IsDeleted = isDeleted;
        enrollmentDate = DateTime.Now;
    }

    [JsonConstructor]
    public Member(int memberId, string name, string phoneNumber, string email, bool isDeleted, DateTime enrollmentDate,
        Membership membership)
    {
        MemberId = memberId;
        Name = name;
        PhoneNumber = phoneNumber;
        Email = email;
        EnrollmentDate = enrollmentDate;
        IsDeleted = isDeleted;
        Membership = membership;
    }
    #endregion

    #region Properties
    public int MemberId
    {
        get => memberId;
        set
        {
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException("MemberId", "MemberId skal være større end 0");
            }

            memberId = value;
        }
    }

    public string Name
    {
        get => name;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentNullException("Name", "Name må ikke være tom");
            }

            name = value;
        }
    }

    public string PhoneNumber
    {
        get => phoneNumber;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                phoneNumber = "Intet telefonnummer oplyst";
                return;
            }

            phoneNumber = value;
        }
    }

    public string Email
    {
        get => email;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                email = "Intet email oplyst";
                return;
            }

            email = value;
        }
    }

    public DateTime EnrollmentDate
    {
        get => enrollmentDate;
        set
        {
            if (value == DateTime.MinValue)
            {
                value = DateTime.Now;
            }

            value = value.Date;
            enrollmentDate = value;

        }
    }

    public bool IsDeleted
    {
        get => isDeleted;
        set
        {
            isDeleted = value;
        }
    }

    public Membership Membership
    {
        get => membership;
        set
        {
            // Just to make sure that the membership is never null. If it is, then we create a new membership with some default values.
            if (value is null)
            {
                value = new Membership(1, "Aktiv", "En random beskrivelse");
            }

            membership = value;
        }
    }
    #endregion
}
using System.Text.Json.Serialization;

namespace Nitrilon.Entities;

public class Membership
{
    #region Fields
    private int membershipId;
    private string name;
    private string description;
    #endregion

    #region Constructors

    //public Membership(string name, string description)
    //{
    //    Name = name;
    //    Description = description;
    //}

    [JsonConstructor]
    public Membership(int membershipId, string name, string description)
    {
        MembershipId = membershipId;
        Name = name;
        Description = description;
    }
    #endregion


    #region Properties
    public int MembershipId
    {
        get { return membershipId; }
        set
        {
            if (value <= 0)
            {
                value = 1;
            }

            if (value != membershipId)
            {
                membershipId = value;
            }
        }
    }

    public string Name
    {
        get { return name; }
        set
        {
            if (string.IsNullOrEmpty(value))
            {
                value = "No name";
            }

            if (name != value)
            {
                name = value;
            }
        }
    }

    public string Description
    {
        get { return description; }
        set
        {
            if (string.IsNullOrEmpty(value))
            {
                value = "No description";
            }

            if (description != value)
            {
                description = value;
            }
        }
    }
    #endregion

    #region Methods
    #endregion
}
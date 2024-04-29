﻿using System.Text.Json.Serialization;

namespace Nitrilon.Entities;

public class Membership
{
    #region Fields
    private int membershipId;
    private string name;
    private string description;
    #endregion

    #region Constructors

    public Membership(string name, string description)
    {
        Name = name;
        Description = description;
    }

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
            if(value <= 0) {
                throw new ArgumentException("Membership isn't valid.");
            }

            membershipId = value;
        }
    }

    public string Name
    {
        get { return name; }
        set { name = value; }
    }

    public string Description
    {
        get { return description; }
        set { description = value; }
    }
    #endregion

    #region Methods
    #endregion
}
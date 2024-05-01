﻿using System.Text.Json.Serialization;

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

    public bool IsDeleted
    {
        get => isDeleted; set => isDeleted = value;
    }

    public Membership Membership
    {
        get => membership; set => membership = value;
    }
    #endregion
}
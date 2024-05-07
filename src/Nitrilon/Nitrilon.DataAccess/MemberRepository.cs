using Microsoft.Data.SqlClient;
using Nitrilon.Entities;

namespace Nitrilon.DataAccess;

public class MemberRepository : Repository
{
    #region Constructors

    /// <summary>
    /// Constructor for the MemberRepository class.
    /// Calling the base class constructor
    /// </summary>
    public MemberRepository() : base()
    {
    }

    #endregion

    #region Methods

    /// <summary>
    /// Method to get all members from the database.
    /// </summary>
    /// <returns>All members</returns>
    /// <exception cref="Exception"></exception>
    public List<Member> GetAllMembers()
    {
        List<Member> members = new List<Member>();

        try
        {
            // Tak til SSMS for join queryen ;)
            string query = @"SELECT dbo.Memberships.Name AS MembershipName, dbo.Memberships.Description AS MembershipDescription, dbo.Members.* FROM dbo.Members INNER JOIN
                         dbo.Memberships ON dbo.Members.MembershipId = dbo.Memberships.MembershipId";

            SqlDataReader reader = Execute(query);

            while (reader.Read())
            {
                int memberId = Convert.ToInt32(reader["MemberId"]);
                string name = Convert.ToString(reader["Name"]);
                string phoneNumber = Convert.ToString(reader["PhoneNumber"]);
                string email = Convert.ToString(reader["Email"]);
                bool isDeleted = Convert.ToBoolean(reader["IsDeleted"]);
                DateTime enrollmentDate = Convert.ToDateTime(reader["EnrollmentDate"]);
                Membership membership = new Membership(Convert.ToInt32(reader["MembershipId"]), Convert.ToString(reader["MembershipName"]), Convert.ToString(reader["MembershipDescription"]));

                Member m = new(memberId, name, phoneNumber, email, isDeleted, enrollmentDate, membership);

                members.Add(m);
            }

            return members;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {
            CloseConnection();
        }
    }

    /// <summary>
    /// Method to create a new member in the database and return the new member.
    /// </summary>
    /// <param name="member">Member object</param>
    /// <returns>New member</returns>
    /// <exception cref="Exception"></exception>
    public Member CreateMember(Member member)
    {
        try
        {
            string query = $"INSERT INTO Members (Name, PhoneNumber, Email, EnrollmentDate, MembershipId) VALUES ('{member.Name}', '{member.PhoneNumber}', '{member.Email}', '{member.EnrollmentDate}', '{member.Membership.MembershipId}')";

            Execute(query);

            return member;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {
            CloseConnection();
        }
    }

    /// <summary>
    /// Method to soft delete a member from the database.
    /// </summary>
    /// <param name="MemberId">MemberId</param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public bool SoftDelete(int MemberId)
    {
        string query = $"UPDATE Members SET IsDeleted = CASE WHEN IsDeleted = 1 THEN 0 ELSE 1 END WHERE MemberId = {MemberId}";

        try
        {
            Execute(query);

            return true;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {
            CloseConnection();
        }
    }

    /// <summary>
    /// Method to receive a full member object, update the member in the database and return the updated member.
    /// </summary>
    /// <param name="member">Member object</param>
    /// <returns>Updated member object</returns>
    /// <exception cref="Exception"></exception>
    public Member UpdateMember(Member member)
    {
        try
        {
            string query = $"UPDATE Members SET Name = '{member.Name}', PhoneNumber = '{member.PhoneNumber}', Email = '{member.Email}', EnrollmentDate = '{member.EnrollmentDate}', MembershipId = {member.Membership.MembershipId} WHERE MemberId = {member.MemberId}";

            Execute(query);

            return member;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {
            CloseConnection();
        }
    }

    /// <summary>
    /// Method to update an existing member's membership in the database.
    /// </summary>
    /// <param name="memberId">The member id of the member that should be updated.</param>
    /// <returns>Nothing</returns>
    /// <exception cref="Exception"></exception>
    public void UpdateMembership(int memberId)
    {
        try
        {
            string query =
                $"UPDATE Members SET MembershipId = CASE WHEN MembershipId = 1 THEN 2 ELSE 1 END WHERE MemberId = {memberId}";

            Execute(query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {
            CloseConnection();
        }
    }
}

#endregion

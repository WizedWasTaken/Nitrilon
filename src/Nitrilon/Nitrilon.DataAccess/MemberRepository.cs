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
                Membership membership = new Membership(Convert.ToString(reader["MembershipName"]), Convert.ToString(reader["MembershipDescription"]));

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

    public Member CreateMember(Member member)
    {
        try
        {
            string query =
                "INSERT INTO Members (Name, PhoneNumber, Email, IsDeleted) VALUES (@Name, @PhoneNumber, @Email, @IsDeleted); SELECT SCOPE_IDENTITY()";

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

    public Member UpdateMember(Member member)
    {
        try
        {
            string query =
                $"UPDATE Members SET Name = @Name, PhoneNumber = @PhoneNumber, Email = @Email WHERE MemberId = @MemberId";

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

    #endregion

}
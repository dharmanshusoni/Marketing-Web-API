using Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Service.Utility;
using DataLayer.UnitOfWork;
using DataLayer.Models;
using AutoMapper;

namespace Service.User
{
    public class UserService : IUserInterface
    {
        SqlConnection con;
        List<UserDTO> _users;
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        //private readonly MarketingToolContext _context;

        public UserService(UnitOfWork unitOfWork, IMapper mapper)
        {
            con = new SqlConnection(Connections.Connect());            
            SqlConnection.ClearAllPools();
            _mapper = mapper;
            _unitOfWork = unitOfWork; 
        }

        public object Login(UserDTO userData)
        {
            List<Result> Result = new List<Result>();
            Result result = new Result();
            string query = string.Format("VerifyUser '" + userData.UserEmailId + "','" + userData.Password + "'");
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    result.message = "Data Found";
                    while (reader.Read())
                    {
                        UserDTO user = new UserDTO();
                        user.UserId = (reader.GetValue(0) != null) ? int.Parse(reader.GetInt32(0).ToString()) : 0;

                        if (user.UserId == -101)
                        {
                            result.message = "No User Found";
                        }
                        else if (user.UserId == -102)
                        {
                            result.message = "InActive User";
                        }
                        else
                        {
                            result.data.Add(user);
                        }
                    }
                }
                else
                {
                    result.message = "No Data Found";
                }
                con.Close();
            }
            result.status = 1;
            result.count = result.data.Count;
            result.data_name = "Login";
            result.generated_on = Base.getInstance().GetEpochOf(DateTimeOffset.Now.UtcDateTime);
            return result;
        }

        public object GetUsersV2()
        {
            string SPVerify = "GetUsers";
            List <SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("@UserId", 0));
            //DataTable dt = SQLExecuter.Execute("Verify", param);
            DataTable dt = SPVerify.Execute(param);
            if (dt.Rows.Count > 0)
            {
                List<UserDTO> user = CollectionHelper.ToList<UserDTO>(dt);
                return user;
            }
            else
            {
                return new List<UserDTO>();
            }
        }

        public Result GetUsers()
        {
            Result result = new Result();
            string query = string.Format("GetUsers");
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    result.message = "Data Found";
                    while (reader.Read())
                    {
                        UserDTO user = new UserDTO();
                        //user.User_Id = (reader.GetValue(0) != null) ? int.Parse(reader.GetInt32(0).ToString()) : 0;
                        //user.User_First_Name = (reader.GetValue(1) != null) ? reader.GetString(1) : string.Empty;
                        //user.User_Last_Name = (reader.GetValue(2) != null) ? reader.GetString(2) : string.Empty;
                        //user.User_Email_Id = (reader.GetValue(3) != null) ? reader.GetString(3) : string.Empty;
                        //user.Password = (reader.GetValue(4) != null) ? reader.GetString(4) : string.Empty;
                        //user.User_Image = (reader.IsDBNull(5)) ? string.Empty : reader.GetString(5);
                        //user.User_Phone = (reader.IsDBNull(6)) ? string.Empty : reader.GetString(6);
                        //user.UserType_Id = (reader.GetValue(7) != null) ? int.Parse(reader.GetInt32(7).ToString()) : 0;
                        //user.Farmer_Id = (reader.GetValue(8) != null) ? int.Parse(reader.GetInt32(8).ToString()) : 0;
                        result.data_name = "Users List";
                        result.data.Add(user);
                    }
                }
                else
                {
                    result.message = "No Data Found";
                }
                con.Close();
            }
            result.status = 1;
            result.count = result.data.Count;
            return result;
        }

        public object GetUserType(int userTypeId)
        {
            Result result = new Result();
            string query = string.Format("GetUserType " + userTypeId);
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    result.message = "Data Found";
                    while (reader.Read())
                    {
                        UserType user = new UserType();
                        user.UserTypeId = (reader.GetValue(0) != null) ? int.Parse(reader.GetInt32(0).ToString()) : 0;
                        user.UserTypeName = (reader.GetValue(1) != null) ? reader.GetString(1) : string.Empty;
                        if (user.UserTypeId == 0)
                        {
                            result.data_name = "User Type List";
                        }
                        else
                        {
                            result.data_name = "User Type Detail";
                        }
                        result.data.Add(user);
                    }
                }
                else
                {
                    result.message = "No Data Found";
                }
                con.Close();
            }
            result.status = 1;
            result.count = result.data.Count;
            return result;
        }

        public object SaveUser(UserDTO userData)
        {
            Result result = new Result();
            string query = string.Format("InUpDeUserProfile");
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@StatementType", "In");
                cmd.Parameters.AddWithValue("@id", 0);
                cmd.Parameters.AddWithValue("@Columns", "");
                cmd.Parameters.AddWithValue("@User_First_Name", ((userData.UserFirstName)).Trim());
                cmd.Parameters.AddWithValue("@User_Last_Name", ((userData.UserLastName)).Trim());
                cmd.Parameters.AddWithValue("@User_Email_Id", ((userData.UserEmailId)).Trim());
                cmd.Parameters.AddWithValue("@Password", ((userData.Password)).Trim());
                cmd.Parameters.AddWithValue("@User_Image", ((userData.UserImage)).Trim());
                cmd.Parameters.AddWithValue("@User_Phone", ((userData.UserPhone)).Trim());
                cmd.Parameters.AddWithValue("@UserType_Id", ((userData.UserTypeId)));

                //var returnParameter = cmd.Parameters.Add("@ErrorCode", SqlDbType.Int);
                //returnParameter.Direction = ParameterDirection.ReturnValue;

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                //var Result = cmd.ExecuteNonQuery();
                //int isExist = (int)cmd.Parameters["@ErrorCode"].Value;

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        UserDTO user = new UserDTO();
                        user.UserId = (reader.GetValue(0) != null) ? int.Parse(reader.GetInt32(0).ToString()) : 0;
                        if (user.UserId == -101)
                        {
                            result.message = "User Already Exist";
                        }
                        else if (user.UserId == -102)
                        {
                            result.message = "InActive User";
                        }
                        else
                        {
                            result.message = "Data Saved";
                            result.data.Add(user);
                        }
                    }
                }
                else
                {
                    result.message = "Unable to process request";
                }
                con.Close();
            }
            result.status = 1;
            result.count = result.data.Count;
            result.data_name = "UserProfile";
            return result;
        }

        public object UpdateUser(UserDTO userData)
        {
            Result result = new Result();
            string query = string.Format("InUpDeUserProfile");
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@StatementType", "Up");
                cmd.Parameters.AddWithValue("@id", userData.UserId);
                cmd.Parameters.AddWithValue("@Columns", "");
                cmd.Parameters.AddWithValue("@User_First_Name", ((userData.UserFirstName)).Trim());
                cmd.Parameters.AddWithValue("@User_Last_Name", ((userData.UserLastName)).Trim());
                cmd.Parameters.AddWithValue("@User_Email_Id", ((userData.UserEmailId)).Trim());
                cmd.Parameters.AddWithValue("@Password", ((userData.Password)).Trim());
                cmd.Parameters.AddWithValue("@User_Image", ((userData.UserImage)).Trim());
                cmd.Parameters.AddWithValue("@User_Phone", ((userData.UserPhone)).Trim());
                cmd.Parameters.AddWithValue("@UserType_Id", ((userData.UserTypeId)));

                //var returnParameter = cmd.Parameters.Add("@ErrorCode", SqlDbType.Int);
                //returnParameter.Direction = ParameterDirection.ReturnValue;

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                //var Result = cmd.ExecuteNonQuery();
                //int isExist = (int)cmd.Parameters["@ErrorCode"].Value;

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        UserDTO user = new UserDTO();
                        user.UserId = (reader.GetValue(0) != null) ? int.Parse(reader.GetInt32(0).ToString()) : 0;
                        if (user.UserId == -101)
                        {
                            result.message = "User Already Exist";
                        }
                        else if (user.UserId== -102)
                        {
                            result.message = "InActive User";
                        }
                        else if (user.UserId == -103)
                        {
                            result.message = "User Not Exist";
                        }
                        else
                        {
                            result.message = "Data Updated";
                            result.data.Add(user);
                        }
                    }
                }
                else
                {
                    result.message = "Unable to process request";
                }
                con.Close();
            }
            result.status = 1;
            result.count = result.data.Count;
            result.data_name = "UserProfile";
            return result;
        }


        #region JWT Token
        public bool IsValidUserCredentials(string userName, string password)
        {
            if (string.IsNullOrWhiteSpace(userName))
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                return false;
            }
            var records = _unitOfWork.UserRepository.Get();
            _users = _mapper.Map<List<DataLayer.Models.User>, List<UserDTO>>(records.ToList());
            var userSelected = _users.Where(u => u.UserEmailId.Trim().ToLower() == userName.Trim().ToLower() && u.Password.Trim().ToLower() == password.Trim().ToLower()).SingleOrDefault();

            return userSelected!=null ? true : false;
        }

        public bool IsAnExistingUser(string userName)
        {
            var records = _unitOfWork.UserRepository.Get();
            _users = _mapper.Map<List<DataLayer.Models.User>, List<UserDTO>>(records.ToList());

            return _users.Any(u => u.UserEmailId.Trim().ToLower() == userName.Trim().ToLower());
        }

        public string GetUserRole(string userName)
        {
            if (!IsAnExistingUser(userName))
            {
                return string.Empty;
            }

            if (userName == "ADMIN")
            {
                return UserRoles.ADMIN;
            }

            return UserRoles.USER;
        }

        public UserDTO getUser(string userName, string password)
        {
            if (!IsAnExistingUser(userName))
            {
                return new UserDTO();
            }
            var records = _unitOfWork.UserRepository.Get();
            _users = _mapper.Map<List<DataLayer.Models.User>, List<UserDTO>>(records.ToList());
            var data = _users.Where(u => u.UserEmailId.Trim().ToLower() == userName.Trim().ToLower() && u.Password.Trim().ToLower() == password.Trim().ToLower()).SingleOrDefault();
            if (data != null)
            {
                return data;
            }
            return new UserDTO();
        }

        #endregion
    }
    public static class UserRoles
    {
        public const string ADMIN = nameof(ADMIN);
        public const string USER = nameof(USER);
        public const string UNKNOWN  = nameof(UNKNOWN);
        public const string SUPERUSER = nameof(SUPERUSER);
    }
}

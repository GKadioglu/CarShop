import React, { useEffect, useState } from "react";
import { fetchWithToken } from "./Shared/api";
import "./Css/AdminUserEdit.css";
import profile from "./Images/accountMenuImages/profile.png";


function AdminUserEdit() {
  const [users, setUsers] = useState([]); 
  const [selectedUser, setSelectedUser] = useState(null); 
  const [loading, setLoading] = useState(true); 
  const [error, setError] = useState(null); 

  useEffect(() => {
    const fetchUsers = async () => {
      try {
        const response = await fetchWithToken("http://localhost:5000/api/admin/getusers");
        if (!response.ok) {
          throw new Error("Kullanıcılar alınırken bir hata oluştu.");
        }
        const data = await response.json();
        setUsers(data);
      } catch (err) {
        setError(err.message);
      } finally {
        setLoading(false);
      }
    };

    fetchUsers();
  }, []);

  if (loading) {
    return <p>Yükleniyor...</p>;
  }

  if (error) {
    return <p>Hata: {error}</p>;
  }

  return (
    <div className="admin-user-list-container">
      {/* Sol taraf: Kullanıcı Listesi */}
      <div className="user-list">
        <h3>Kullanıcılar</h3>
        <ul>
          {users.map((user) => (
            <li key={user.userId} onClick={() => setSelectedUser(user)}>
              {user.userName}
            </li>
          ))}
        </ul>
      </div>

      {/* Sağ taraf: Kullanıcı Detayları */}
      <div className="user-details">
        {selectedUser ? (
          <div className="user-card">
            <img
              src={profile} // Profil resmi statik
              className="profile-picture"
            />
            <h3>
              {selectedUser.firstName} {selectedUser.lastName}
            </h3>
            <p>
              <strong>ID:</strong> {selectedUser.userId}
            </p>
            <p>
              <strong>Email:</strong> {selectedUser.email}
            </p>
            <p>
              <strong>Email Onayı:</strong>{" "}
              {selectedUser.emailConfirmed ? "Evet" : "Hayır"}
            </p>

          </div>
        ) : (
          <p>Lütfen bir kullanıcı seçin.</p>
        )}
      </div>
    </div>
  );
}

export default AdminUserEdit;
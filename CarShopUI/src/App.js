import "./App.css";
import Layout from "./Components/Shared/Layout";
import HomePage from "./Components/HomePage";
import CarDetails from "./Components/Details";
import Category from "./Components/Category";
import About from "./Components/About";
import CategoryDetails from "./Components/CategoryDetails";
import SearchResults from "../src/Components/Shared/SearchResults";
import Register from "./Components/Register";
import ContactCar from "./Components/ContactCar";
import MakeAnOffer from "./Components/MakeAnOffer";
import Login from "./Components/Login";
import ConfirmedEmail from "./Components/ConfirmedEmail";
import MyAccount from "./Components/MyAccount";
import PrivateRoute from "./Components/Shared/PrivateRoute";
import Admin from "./Components/Admin";
import AdminCarEdit from "./Components/AdminCarEdit";
import AdminUserEdit from "./Components/AdminUserEdit";
import AdminMessage from "./Components/AdminMessage";
import AdminReplyOffer from "./Components/AdminReplyOffer";
import AdminNotification from "./Components/AdminNotification";
import Blog from "./Components/Blog";
import Notifications from "./Components/Notifications";
import Agreements from "./Components/Agreements";
import Favourite from "./Components/Favourite";
import AccountSettings from "./Components/AccountSettings";
import Messages from "./Components/Message";
import Help from "./Components/Help";
import { AuthProvider } from "./Components/Shared/AuthContext";
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import ScrollToTop from "./Components/Shared/ScrollToTop";

function App() {
  return (
    <AuthProvider>
      <Router>
        <ScrollToTop />
        <Layout>
          <Routes>
            <Route path="/" element={<HomePage />} />
            <Route path="/car/:carId" element={<CarDetails />} />
            <Route path="/category" element={<Category />} />
            <Route
              path="/category/:categoryName"
              element={<CategoryDetails />}
            />
            <Route path="/about" element={<About />} />
            <Route path="/search" element={<SearchResults />} />
            <Route path="/contact/:carName" element={<ContactCar />} />
            <Route path="/makeAnOffer/:carName" element={<MakeAnOffer />} />
            <Route path="/register" element={<Register />} />
            <Route path="/login" element={<Login />} />
            <Route path="/blog" element={<Blog />} />
            <Route path="/message" element={<Messages />} />
            <Route path="/favourite" element={<Favourite />} />
            <Route path="/agreements" element={<Agreements />} />
            <Route path="/notifications" element={<Notifications />} />
            <Route path="/help" element={<Help />} />
            <Route path="/accountSettings" element={<AccountSettings />} />
            <Route
              path="/admin"
              element={
                <PrivateRoute roles={["admin"]}>
                  <Admin />
                </PrivateRoute>
              }
            />
            <Route
              path="/admin/caredit"
              element={
                <PrivateRoute roles={["admin"]}>
                  <AdminCarEdit />
                </PrivateRoute>
              }
            />
            <Route
              path="/admin/replyoffer"
              element={
                <PrivateRoute roles={["admin"]}>
                  <AdminReplyOffer />
                </PrivateRoute>
              }
            />
            <Route
              path="/admin/messages"
              element={
                <PrivateRoute roles={["admin"]}>
                  <AdminMessage />
                </PrivateRoute>
              }
            />
            <Route
              path="/admin/notification"
              element={
                <PrivateRoute roles={["admin"]}>
                  <AdminNotification />
                </PrivateRoute>
              }
            />
            <Route
              path="/admin/useredit"
              element={
                <PrivateRoute roles={["admin"]}>
                  <AdminUserEdit />
                </PrivateRoute>
              }
            />
            <Route path="/confirmedemail" element={<ConfirmedEmail />} />
            <Route path="/myAccount/:userName" element={<MyAccount />} />
          </Routes>
        </Layout>
      </Router>
    </AuthProvider>
  );
}

export default App;

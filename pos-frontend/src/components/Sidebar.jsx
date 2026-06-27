import { Link ,NavLink} from "react-router-dom";
import "../styles/navbar.css";
import {
 FaChartPie,
 FaBox,
 FaTags,
 FaUsers,
 FaTruck,
 FaMoneyBill
} from "react-icons/fa";

export default function Sidebar() {

  return (

    <div className="sidebar">

      <h2 className="logo">
        🛒 POS
      </h2>

      <Link to="/dashboard">
        <FaChartPie /> Dashboard
      </Link>

      <Link to="/products">
        <FaBox /> Products
      </Link>

      <Link to="/categories">
        <FaTags /> Categories
      </Link>

      <Link to="/customers">
        <FaUsers /> Customers
      </Link>

      <Link to="/suppliers">
        <FaTruck /> Suppliers
      </Link>
       <Link to="/purchase">
        <FaTruck /> Purchase
      </Link>
      <Link to="/sales">
        <FaMoneyBill /> Sales
      </Link>
      {/* <div className="sidebar-title">Reports</div>

            <NavLink to="/reports/sales">
                📊 Sales Report
            </NavLink>

            <NavLink to="/reports/profit">
                📈 Profit Report
            </NavLink>

            <NavLink to="/reports/stock">
                📦 Stock Report
            </NavLink>

            <NavLink to="/reports/inventory">
                🏬 Inventory Report
            </NavLink> */}
{/* <Link to="/reports">
        <FaTruck /> Reports
      </Link> */}

    </div>

  );
}
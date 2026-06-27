//import { BrowserRouter, Routes, Route } from "react-router-dom";
import { HashRouter, Routes, Route } from "react-router-dom";

import Login from "../pages/Login";
import Dashboard from "../pages/Dashboard";
import Products from "../pages/Products";
import Categories from "../pages/Categories";
import Customers from "../pages/Customers";
import Suppliers from "../pages/Suppliers";
import Purchase from "../pages/Purchases";
import Sale from "../pages/Sale";
import SalesReport from "../pages/reports/SalesReport";
import ProfitReport from "../pages/reports/ProfitReport";
import StockReport from "../pages/reports/StockReport";
import InventoryReport from "../pages/reports/InventoryReport";
export default function AppRoutes() {
  return (
    <HashRouter >

      <Routes>

        <Route
          path="/"
          element={<Login />}
        />

        <Route
          path="/dashboard"
          element={<Dashboard />}
        />

        <Route
          path="/products"
          element={<Products />}
        />
 <Route
          path="/Categories"
          element={<Categories />}
        />

         <Route
          path="/Customers"
          element={<Customers />}
        />

          <Route
          path="/Suppliers"
          element={<Suppliers />}
        />

         <Route
          path="/Purchase"
          element={<Purchase />}
        />
    <Route path="/sales" element={<Sale />} />
        
        <Route path="/reports/sales" element={<SalesReport />} />
                <Route path="/reports/profit" element={<ProfitReport />} />
                <Route path="/reports/stock" element={<StockReport />} />
                <Route path="/reports/inventory" element={<InventoryReport />} />
      </Routes>

    </HashRouter >
  );
}
// import { useEffect, useState } from "react";
// import api from "../api/axios";
// import MainLayout from "../layouts/MainLayout";
// import "../styles/sidebar.css"
// import {
//   ResponsiveContainer,
//   LineChart,
//   Line,
//   XAxis,
//   YAxis,
//   Tooltip,
//   CartesianGrid,
// } from "recharts";
// import "./dashboard.css";

// export default function Dashboard() {
//   const [summary, setSummary] = useState(null);
//   const [profit, setProfit] = useState(null);
//   const [lowStock, setLowStock] = useState([]);
//   const [salesTrend, setSalesTrend] = useState([]);
//   const [loading, setLoading] = useState(true);

//   useEffect(() => {
//     loadData();
//   }, []);

//   const loadData = async () => {
//     try {
//       setLoading(true);

//       const [summaryRes, profitRes, lowStockRes, trendRes] =
//         await Promise.all([
//           api.get("/dashboard/summary"),
//           api.get("/dashboard/profit"),
//           api.get("/inventory/low-stock"),
//           api.get("/dashboard/sales-trend?days=7"),
//         ]);

//       setSummary(summaryRes.data);
//       setProfit(profitRes.data);
//       setLowStock(lowStockRes.data);
//       setSalesTrend(trendRes.data);
//     } catch (err) {
//       console.log(err);
//     } finally {
//       setLoading(false);
//     }
//   };

//   const Card = ({ title, value, icon, color }) => (
//     <div className={`card ${color}`}>
//       <div className="card-top">
//         <span>{icon}</span>
//         <h4>{title}</h4>
//       </div>
//       <h2>{value ?? 0}</h2>
//     </div>
//   );

//   return (
//     <MainLayout>
//     <div className="dashboard">

//       {/* HEADER */}
//       <div className="header">
//         <h1>📊 POS Dashboard</h1>
//         <p>Real-time business overview</p>
//       </div>

//       {/* KPI CARDS */}
//       <div className="grid-cards">

//         <Card
//           title="Total Sales"
//           value={summary?.totalSales}
//           icon="💰"
//           color="blue"
//         />

//         <Card
//           title="Profit"
//           value={profit?.grossProfit || profit?.profit}
//           icon="📈"
//           color="green"
//         />

//         <Card
//           title="Customers"
//           value={summary?.totalCustomers}
//           icon="👤"
//           color="purple"
//         />

//         <Card
//           title="Products"
//           value={summary?.totalProducts}
//           icon="📦"
//           color="orange"
//         />
//       </div>

//       {/* MAIN GRID */}
//       <div className="main-grid">

//         {/* CHART */}
//         <div className="panel">
//           <div className="panel-title">
//             <h3>Sales Trend</h3>
//             <span>Last 7 days</span>
//           </div>

//           <ResponsiveContainer width="100%" height={280}>
//             <LineChart data={salesTrend}>
//               <CartesianGrid strokeDasharray="3 3" opacity={0.2} />
//               <XAxis dataKey="date" />
//               <YAxis />
//               <Tooltip />
//               <Line
//                 type="monotone"
//                 dataKey="totalSales"
//                 stroke="#60a5fa"
//                 strokeWidth={3}
//               />
//             </LineChart>
//           </ResponsiveContainer>
//         </div>

//         {/* LOW STOCK */}
//         <div className="panel">
//           <div className="panel-title">
//             <h3>Low Stock</h3>
//             <span>Alert items</span>
//           </div>

//           {loading ? (
//             <p className="muted">Loading...</p>
//           ) : lowStock.length === 0 ? (
//             <p className="ok">All stock healthy 🎉</p>
//           ) : (
//             lowStock.map((item) => (
//               <div key={item.productId} className="stock-row">
//                 <div>
//                   <p className="name">{item.productName}</p>
//                   <small className="muted">
//                     Min: {item.minStockLevel}
//                   </small>
//                 </div>

//                 <div className="badge-danger">
//                   {item.currentStock}
//                 </div>
//               </div>
//             ))
//           )}
//         </div>

//       </div>
//     </div>
//      </MainLayout>
//   );
// }
// import { useEffect, useState } from "react";
// import { useEffect, useState } from "react";
// import api from "../api/axios";
// import MainLayout from "../layouts/MainLayout";
// import  "./dashboard.css";
// import InventoryCards from "../components/dashboard/InventoryCards";
// import TopProducts from "../components/dashboard/TopProducts";
// import LowStockAlert from "../components/dashboard/LowStockAlert";
// import "../styles/sidebar.css"
// import {
//   ResponsiveContainer,
//   LineChart,
//   Line,
//   XAxis,
//   YAxis,
//   Tooltip,
//   CartesianGrid,
// } from "recharts";
// export default function Dashboard() {

//     const [inventory, setInventory] = useState(null);
//     const [summary, setSummary] = useState(null);
//     const [topProducts, setTopProducts] = useState([]);
//     const [lowStock, setLowStock] = useState([]);

//     useEffect(() => {
//         loadDashboard();
//     }, []);

//     const loadDashboard = async () => {
//         try {
//             const inv = await api.get("/dashboard/inventory-summary");
//             const sum = await api.get("/dashboard/summary");
//             const top = await api.get("/dashboard/top-selling-products");
//             const low = await api.get("/inventory/low-stock");

//             setInventory(inv.data);
//             setSummary(sum.data);
//             setTopProducts(top.data);
//             setLowStock(low.data);

//         } catch (err) {
//             console.log(err);
//         }
//     };

//     return (
//         <MainLayout>

//             <h2 style={{ color: "white", marginBottom: 20 }}>
//                 📊 Dashboard Overview
//             </h2>

//             {/* KPI CARDS */}
//             <InventoryCards inventory={inventory} summary={summary} />

//             {/* LOW STOCK ALERT */}
//             <LowStockAlert data={lowStock} />

//             {/* TOP PRODUCTS */}
//             <TopProducts data={topProducts} />

//         </MainLayout>
//     );
// }

import { useEffect, useState } from "react";
import MainLayout from "../layouts/MainLayout";
import api from "../api/axios";

import KpiGrid from "../components/dashboard/KpiGrid";
import SalesPurchaseChart from "../components/dashboard/SalesPurchaseChart";
import ProfitChart from "../components/dashboard/ProfitChart";
import TopProducts from "../components/dashboard/TopProducts";
import LowStockAlerts from "../components/dashboard/LowStockAlerts";
import LiveNotifications from "../components/dashboard/LiveNotifications";
import InventoryValueCard from "../components/dashboard/InventoryValueCard";
import RecentSales from "../components/dashboard/RecentSales";
import "./dashboard.css";
import "../styles/sidebar.css"

//import { startSignalR } from "../services/signalr";
import { startSignalRConnection } from "../services/signalr";
import "../styles/pages/dashboard.css";

export default function Dashboard() {

    const [summary, setSummary] = useState(null);
    const [topProducts, setTopProducts] = useState([]);
    const [lowStock, setLowStock] = useState([]);
    const [salesTrend, setSalesTrend] = useState([]);
    // const [profit, setProfit] = useState(null);
    const [profitSummary, setProfitSummary] = useState(null);
const [profitTrend, setProfitTrend] = useState([]);
    const [inventoryValue, setInventoryValue] = useState(null);
    const [recentSales, setRecentSales] = useState([]);
    const [notifications, setNotifications] = useState([]);

    useEffect(() => {
        loadDashboard();

        // 🔥 REAL TIME SIGNALR
        startSignalRConnection((msg) => {
            setNotifications(prev => [msg, ...prev]);
            loadDashboard(); // auto refresh dashboard
        });

    }, []);

    const loadDashboard = async () => {
        try {
            // const [
            //     sumRes,
            //     topRes,
            //     lowRes,
            //     trendRes,
            //     profitRes,
            //     invRes,
            //     salesRes
            // ] = await Promise.all([
            //     api.get("/dashboard/Summary"),
            //     api.get("/dashboard/top-selling-products"),
            //     api.get("/inventory/low-stock"),
            //     api.get("/dashboard/sales-trend?days=7"),
            //     api.get("/inventory/profit"),
            //     api.get("/inventory/valuation"),
            //     api.get("/sale")
            // ]);
            const [
    sumRes,
    topRes,
    lowRes,
    trendRes,
    profitRes,
    invRes,
    salesRes,
    profitDailyRes
] = await Promise.all([
    api.get("/dashboard/Summary"),
    api.get("/dashboard/top-selling-products"),
    api.get("/inventory/low-stock"),
    api.get("/dashboard/sales-trend?days=7"),
    api.get("/inventory/profit"),
    api.get("/inventory/valuation"),
    api.get("/sale"),
    api.get("/inventory/profit/daily")
]);

            setSummary(sumRes.data);
            setTopProducts(topRes.data);
            setLowStock(lowRes.data);
            setSalesTrend(trendRes.data);
            // setProfit(profitRes.data);
            setProfitSummary(profitRes.data);
setProfitTrend(profitDailyRes.data);
            setInventoryValue(invRes.data);
            setRecentSales(salesRes.data.slice(0, 5));

        } catch (err) {
            console.log(err);
        }
    };

    return (
        <MainLayout>

            <div className="dashboard">

                {/* 🔔 LIVE NOTIFICATIONS */}
                <LiveNotifications data={notifications} />

                {/* KPI CARDS */}
                <KpiGrid summary={summary} />

                {/* INVENTORY VALUE */}
                <InventoryValueCard data={inventoryValue} />

                {/* CHARTS */}
                <div className="charts-grid">

                    {/* <SalesTrendChart data={salesTrend} /> */}
<SalesPurchaseChart data={salesTrend} />
                    <ProfitChart data={profitTrend} />

                </div>

                {/* TABLES */}
                <div className="bottom-grid">

                    <TopProducts data={topProducts} />

                    <LowStockAlerts data={lowStock} />

                </div>

                {/* RECENT SALES */}
                <RecentSales data={recentSales} />

            </div>

        </MainLayout>
    );
}
// import { useEffect, useState } from "react";
// import MainLayout from "../layouts/MainLayout";
// import api from "../api/axios";

// import StatCard from "../components/dashboard/StatCard";
// import SalesPurchaseChart from "../components/dashboard/SalesPurchaseChart";
// import ProfitChart from "../components/dashboard/ProfitChart";
// import TopProducts from "../components/dashboard/TopProducts";
// import LowStockAlerts from "../components/dashboard/LowStockAlerts";
// import   './dashboard.css'
// import "../styles/pages/dashboard.css";
// import "../styles/sidebar.css"
// export default function Dashboard() {

//     const [summary, setSummary] = useState(null);
//     const [topProducts, setTopProducts] = useState([]);
//     const [lowStock, setLowStock] = useState([]);
//     const [salesTrend, setSalesTrend] = useState([]);
//     const [profit, setProfit] = useState(null);

//     useEffect(() => {
//         loadDashboard();
//     }, []);

//     const loadDashboard = async () => {
//         try {
//             const [
//                 sumRes,
//                 topRes,
//                 lowRes,
//                 trendRes,
//                 profitRes
//             ] = await Promise.all([
//                 api.get("/dashboard/Summary"),
//                 api.get("/dashboard/top-selling-products"),
//                 api.get("/inventory/low-stock"),
//                 api.get("/dashboard/sales-trend?days=7"),
//                 api.get("/Inventory/profit")
//             ]);

//             setSummary(sumRes.data);
//             setTopProducts(topRes.data);
//             setLowStock(lowRes.data);
//             setSalesTrend(trendRes.data);
//             setProfit(profitRes.data);

//         } catch (err) {
//             console.log(err);
//         }
//     };

//     return (
//         <MainLayout>

//             <div className="dashboard">

//                 {/* KPI CARDS */}
//                 <div className="stats-grid">

//                     <StatCard title="Today Sales" value={summary?.todaySales} />
//                     <StatCard title="Today Purchases" value={summary?.todayPurchases} />
//                     <StatCard title="Products" value={summary?.totalProducts} />
//                     <StatCard title="Low Stock" value={summary?.lowStockProducts} />

//                 </div>

//                 {/* CHARTS */}
//                 <div className="charts-grid">

//                     <SalesPurchaseChart data={salesTrend} />

//                     <ProfitChart data={profit} />

//                 </div>

//                 {/* TABLES */}
//                 <div className="bottom-grid">

//                     <TopProducts data={topProducts} />

//                     <LowStockAlerts data={lowStock} />

//                 </div>

//             </div>

//         </MainLayout>
//     );
// }
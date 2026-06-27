import StatCard from "./StatCard";

export default function KpiGrid({ summary }) {
    return (
        <div className="stats-grid">

            <StatCard title="Today Sales" value={summary?.todaySales} />
            <StatCard title="Today Purchases" value={summary?.todayPurchases} />
            <StatCard title="Products" value={summary?.totalProducts} />
            <StatCard title="Low Stock" value={summary?.lowStockProducts} />
            <StatCard title="Monthly Sales" value={summary?.monthSales} />
            <StatCard title="Customers" value={summary?.totalCustomers} />

        </div>
    );
}
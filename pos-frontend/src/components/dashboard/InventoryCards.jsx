export default function InventoryCards({ inventory, summary }) {

    if (!inventory || !summary) return null;

    return (
        <div className="dashboard-grid">

            <div className="card">
                <h3>Total Products</h3>
                <p>{inventory.totalProducts}</p>
            </div>

            <div className="card red">
                <h3>Out of Stock</h3>
                <p>{inventory.outOfStockProducts}</p>
            </div>

            <div className="card orange">
                <h3>Low Stock</h3>
                <p>{inventory.lowStockProducts}</p>
            </div>

            <div className="card green">
                <h3>Inventory Value</h3>
                <p>{inventory.inventoryValue}</p>
            </div>

            <div className="card blue">
                <h3>Today Sales</h3>
                <p>{summary.todaySales}</p>
            </div>

            <div className="card purple">
                <h3>Month Sales</h3>
                <p>{summary.monthSales}</p>
            </div>

        </div>
    );
}
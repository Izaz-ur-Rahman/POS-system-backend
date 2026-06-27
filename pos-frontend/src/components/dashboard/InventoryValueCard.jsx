export default function InventoryValueCard({ data }) {
    return (
        <div className="inventory-card">
            <h3>Inventory Value</h3>
            <h1>PKR {data?.totalInventoryValue}</h1>
        </div>
    );
}
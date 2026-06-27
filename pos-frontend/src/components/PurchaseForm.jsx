import { useEffect, useState } from "react";
import api from "../api/axios";

export default function PurchaseForm({ onClose, onSuccess }) {

    const [suppliers, setSuppliers] = useState([]);
    const [products, setProducts] = useState([]);

    const [supplierId, setSupplierId] = useState("");
    const [discount, setDiscount] = useState(0);
    const [taxPercentage, setTaxPercentage] = useState(0);

    const [items, setItems] = useState([
        { productId: "", quantity: 1, purchasePrice: 0 }
    ]);

    useEffect(() => {
        loadData();
    }, []);

    const loadData = async () => {
        const s = await api.get("/supplier");
        const p = await api.get("/product");

        setSuppliers(s.data);
        setProducts(p.data);
    };

    const addRow = () => {
        setItems([...items, { productId: "", quantity: 1, purchasePrice: 0 }]);
    };

    const handleChange = (index, field, value) => {
        const updated = [...items];
        updated[index][field] = value;
        setItems(updated);
    };

    const calculateTotal = () => {
        const subTotal = items.reduce(
            (sum, i) => sum + (i.quantity * i.purchasePrice),
            0
        );

        const discountAmount = (subTotal * discount) / 100;
        const afterDiscount = subTotal - discountAmount;
        const taxAmount = (afterDiscount * taxPercentage) / 100;

        return afterDiscount + taxAmount;
    };

    const submit = async () => {

        try {
            await api.post("/purchase", {
                supplierId,
                discount,
                taxPercentage,
                items
            });

            onSuccess();
            onClose();

        } catch (err) {
            console.log(err);
        }
    };

  return (
    <div className="modal-overlay">

        <div className="modal-box purchase-modal">

            <h2>Create Purchase</h2>

            {/* GRID TOP SECTION */}
            <div className="form-grid-2">

                {/* Supplier */}
                <div>
                    <label>Supplier</label>
                    <select onChange={(e) => setSupplierId(e.target.value)}>
                        <option>Select Supplier</option>
                        {suppliers.map(s => (
                            <option key={s.id} value={s.id}>
                                {s.name}
                            </option>
                        ))}
                    </select>
                </div>

                {/* Discount */}
                <div>
                    <label>Discount %</label>
                    <input
                        type="number"
                        onChange={(e) => setDiscount(Number(e.target.value))}
                    />
                </div>

                {/* Tax */}
                <div>
                    <label>Tax %</label>
                    <input
                        type="number"
                        onChange={(e) => setTaxPercentage(Number(e.target.value))}
                    />
                </div>

                {/* Total Display */}
                <div>
                    <label>Total</label>
                    <div className="total-box">
                        {calculateTotal()}
                    </div>
                </div>

            </div>

            <hr />

            {/* PRODUCT TABLE HEADER */}
            <div className="product-header">
                <span>Product</span>
                <span>Qty</span>
                <span>Price</span>
                <span>Action</span>
            </div>

            {/* PRODUCT ROWS */}
            {items.map((item, index) => (
                <div className="product-row" key={index}>

                    {/* Product */}
                    <select
                        onChange={(e) =>
                            handleChange(index, "productId", e.target.value)
                        }
                    >
                        <option>Select</option>
                        {products.map(p => (
                            <option key={p.id} value={p.id}>
                                {p.name}
                            </option>
                        ))}
                    </select>

                    {/* Qty */}
                    <input
                        type="number"
                        onChange={(e) =>
                            handleChange(index, "quantity", Number(e.target.value))
                        }
                    />

                    {/* Price */}
                    <input
                        type="number"
                        onChange={(e) =>
                            handleChange(index, "purchasePrice", Number(e.target.value))
                        }
                    />

                    {/* Remove Button */}
                    <button
                        className="remove-btn"
                        onClick={() => {
                            const newItems = items.filter((_, i) => i !== index);
                            setItems(newItems);
                        }}
                    >
                        ❌
                    </button>

                </div>
            ))}

            {/* ADD ROW BUTTON */}
            <button className="add-row-btn" onClick={addRow}>
                + Add Product
            </button>

            {/* ACTIONS */}
            <div className="btns">
                <button onClick={submit} className="save">
                    Save Purchase
                </button>

                <button onClick={onClose} className="cancel">
                    Cancel
                </button>
            </div>

        </div>

    </div>
);
}
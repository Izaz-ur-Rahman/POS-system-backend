import { useEffect, useState } from "react";
import api from "../api/axios";

export default function PurchaseDetails({ id, onClose }) {

    const [data, setData] = useState(null);

    useEffect(() => {
        load();
    }, []);

    const load = async () => {
        const res = await api.get(`/purchase/${id}`);
        setData(res.data);
    };

    if (!data) return null;

    return (
        <div className="modal-overlay">

            <div className="modal-box">

                <h2>Purchase Details</h2>

                <p><b>Supplier:</b> {data.supplierName}</p>
                <p><b>Date:</b> {new Date(data.purchaseDate).toLocaleString()}</p>
                <p><b>Total:</b> {data.totalAmount}</p>

                <hr />

                {data.items.map((i, index) => (
                    <div key={index} className="item-row">
                        <p>{i.productName}</p>
                        <p>{i.quantity} x {i.purchasePrice}</p>
                        <p>{i.subTotal}</p>
                    </div>
                ))}

                <button onClick={onClose} className="cancel">
                    Close
                </button>

            </div>

        </div>
    );
}
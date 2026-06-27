// import { useEffect, useState } from "react";
// import api from "../api/axios";

// export default function SaleForm({ onClose, onSuccess }) {

//     const [customers, setCustomers] = useState([]);
//     const [products, setProducts] = useState([]);

//     const [form, setForm] = useState({
//         customerId: 0,
//         discount: 0,
//         taxPercentage: 0,
//         cashReceived: 0,
//         items: [
//             { productId: 0, quantity: 1 }
//         ]
//     });

//     useEffect(() => {
//         loadData();
//     }, []);

//     const loadData = async () => {
//         const c = await api.get("/customer");
//         const p = await api.get("/product");

//         setCustomers(c.data);
//         setProducts(p.data);
//     };

//     const addRow = () => {
//         setForm({
//             ...form,
//             items: [...form.items, { productId: 0, quantity: 1 }]
//         });
//     };

//     const handleChange = (index, field, value) => {
//         const updated = [...form.items];
//         updated[index][field] = value;
//         setForm({ ...form, items: updated });
//     };

//     const submit = async (e) => {
//         e.preventDefault();

//         await api.post("/sale", form);

//         onSuccess();
//         onClose();
//     };

//     return (
//         <div className="modal-overlay">

//             <div className="modal-box sale-modal">

//                 <h2>Create Sale</h2>

//                 {/* CUSTOMER */}
//                 <select
//                     onChange={(e) =>
//                         setForm({ ...form, customerId: Number(e.target.value) })
//                     }
//                 >
//                     <option value="0">Select Customer</option>
//                     {customers.map(c => (
//                         <option key={c.id} value={c.id}>
//                             {c.name}
//                         </option>
//                     ))}
//                 </select>

//                 {/* DISCOUNT + TAX */}
//                 <div className="form-grid-2">
//                     <input
//                         placeholder="Discount"
//                         onChange={(e) =>
//                             setForm({ ...form, discount: Number(e.target.value) })
//                         }
//                     />

//                     <input
//                         placeholder="Tax %"
//                         onChange={(e) =>
//                             setForm({ ...form, taxPercentage: Number(e.target.value) })
//                         }
//                     />
//                 </div>

//                 {/* CASH */}
//                 <input
//                     placeholder="Cash Received"
//                     onChange={(e) =>
//                         setForm({ ...form, cashReceived: Number(e.target.value) })
//                     }
//                 />

//                 {/* ITEMS */}
//                 <h4>Products</h4>

//                 {form.items.map((item, i) => (
//                     <div className="product-row" key={i}>

//                         <select
//                             onChange={(e) =>
//                                 handleChange(i, "productId", Number(e.target.value))
//                             }
//                         >
//                             <option>Select Product</option>
//                             {products.map(p => (
//                                 <option key={p.id} value={p.id}>
//                                     {p.name}
//                                 </option>
//                             ))}
//                         </select>

//                         <input
//                             type="number"
//                             placeholder="Qty"
//                             onChange={(e) =>
//                                 handleChange(i, "quantity", Number(e.target.value))
//                             }
//                         />

//                     </div>
//                 ))}

//                 <button className="add-row-btn" onClick={addRow}>
//                     + Add Product
//                 </button>

//                 {/* ACTIONS */}
//                 <div className="btns">
//                     <button className="save" onClick={submit}>
//                         Complete Sale
//                     </button>

//                     <button className="cancel" onClick={onClose}>
//                         Cancel
//                     </button>
//                 </div>

//             </div>

//         </div>
//     );
// }
import { useEffect, useState } from "react";
import api from "../api/axios";

export default function SaleForm({ onClose, onSuccess }) {

    const [customers, setCustomers] = useState([]);
    const [products, setProducts] = useState([]);

    const [errorMsg, setErrorMsg] = useState("");   // ✅ NEW

    const [form, setForm] = useState({
        customerId: 0,
        discount: 0,
        taxPercentage: 0,
        cashReceived: 0,
        items: [
            { productId: 0, quantity: 1 }
        ]
    });

    useEffect(() => {
        loadData();
    }, []);

    const loadData = async () => {
        const c = await api.get("/customer");
        const p = await api.get("/product");

        setCustomers(c.data);
        setProducts(p.data);
    };

    const addRow = () => {
        setForm({
            ...form,
            items: [...form.items, { productId: 0, quantity: 1 }]
        });
    };

    const handleChange = (index, field, value) => {
        const updated = [...form.items];
        updated[index][field] = value;
        setForm({ ...form, items: updated });
    };

    // const submit = async (e) => {
    //     e.preventDefault();

    //     try {
    //         setErrorMsg(""); // clear old error

    //         await api.post("/sale", form);

    //         onSuccess();
    //         onClose();

    //     } catch (err) {

    //         // ✅ SAFE ERROR HANDLING
    //         const msg =
    //             err?.response?.data?.message ||
    //             "Sale failed. Please try again.";

    //         setErrorMsg(msg);
    //     }
    // };
const submit = async (e) => {
    e.preventDefault();

    try {
        setErrorMsg("");

        const payload = {
            ...form,
            customerId: form.customerId === 0 ? null : form.customerId
        };

        await api.post("/sale", payload);

        onSuccess();
        onClose();

    } catch (err) {
        const msg =
            err?.response?.data?.message ||
            "Sale failed. Please try again.";

        setErrorMsg(msg);
    }
};
    return (
        <div className="modal-overlay">

            <div className="modal-box sale-modal">

                {/* ❌ ERROR ALERT UI */}
                {errorMsg && (
    <div className="error-banner">
        
        <div className="error-content">
            <div className="error-title">⚠ Sale Error</div>
            <div className="error-message">{errorMsg}</div>
        </div>

        <button
            className="error-close"
            onClick={() => setErrorMsg("")}
        >
            ✖
        </button>

    </div>
)}
                <h2>Create Sale</h2>

                {/* CUSTOMER */}
                <select
                    onChange={(e) =>
                        setForm({ ...form, customerId: Number(e.target.value) })
                    }
                >
                    <option value="0">Walk-in Customer</option>
                    {customers.map(c => (
                        <option key={c.id} value={c.id}>
                            {c.name}
                        </option>
                    ))}
                </select>

                {/* DISCOUNT + TAX */}
                <div className="form-grid-2">
                    <input
                        placeholder="Discount"
                        onChange={(e) =>
                            setForm({ ...form, discount: Number(e.target.value) })
                        }
                    />

                    <input
                        placeholder="Tax %"
                        onChange={(e) =>
                            setForm({ ...form, taxPercentage: Number(e.target.value) })
                        }
                    />
                </div>

                {/* CASH */}
                <input
                    placeholder="Cash Received"
                    onChange={(e) =>
                        setForm({ ...form, cashReceived: Number(e.target.value) })
                    }
                />

                {/* ITEMS */}
                <h4>Products</h4>

                {form.items.map((item, i) => (
                    <div className="product-row" key={i}>

                        <select
                            onChange={(e) =>
                                handleChange(i, "productId", Number(e.target.value))
                            }
                        >
                            <option>Select Product</option>
                            {products.map(p => (
                                <option key={p.id} value={p.id}>
                                    {p.name}
                                </option>
                            ))}
                        </select>

                        <input
                            type="number"
                            placeholder="Qty"
                            onChange={(e) =>
                                handleChange(i, "quantity", Number(e.target.value))
                            }
                        />

                    </div>
                ))}

                <button className="add-row-btn" onClick={addRow}>
                    + Add Product
                </button>

                {/* ACTIONS */}
                <div className="btns">

                    <button className="save" onClick={submit}>
                        Complete Sale
                    </button>

                    <button className="cancel" onClick={onClose}>
                        Cancel
                    </button>

                </div>

            </div>

        </div>
    );
}
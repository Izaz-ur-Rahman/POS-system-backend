// import { useState, useEffect } from "react";
// import api from "../api/axios";
// import "../styles/productForm.css";

// export default function ProductForm({
//   show,
//   onClose,
//   onSuccess,
//   editData,
// }) {
//   const [name, setName] = useState("");
//   const [barcode, setBarcode] = useState("");
//   const [purchasePrice, setPurchasePrice] = useState("");
//   const [salePrice, setSalePrice] = useState("");
//   const [stockQuantity, setStockQuantity] = useState("");
//   const [minStockLevel, setMinStockLevel] = useState("");
//   const [categoryId, setCategoryId] = useState("");
//   const [isActive, setIsActive] = useState(true);
//   const [image, setImage] = useState(null);

//   const [categories, setCategories] = useState([]);

//   useEffect(() => {
//     api.get("/category").then((res) => setCategories(res.data));
//   }, []);

//   useEffect(() => {
//     if (editData) {
//       setName(editData.name || "");
//       setBarcode(editData.barcode || "");
//       setPurchasePrice(editData.purchasePrice || "");
//       setSalePrice(editData.salePrice || "");
//       setStockQuantity(editData.stockQuantity || "");
//       setMinStockLevel(editData.minStockLevel || "");
//       setCategoryId(editData.categoryId || "");
//       setIsActive(editData.isActive ?? true);
//     } else {
//       resetForm();
//     }
//   }, [editData]);

//   const resetForm = () => {
//     setName("");
//     setBarcode("");
//     setPurchasePrice("");
//     setSalePrice("");
//     setStockQuantity("");
//     setMinStockLevel("");
//     setCategoryId("");
//     setIsActive(true);
//     setImage(null);
//   };

//   const handleSubmit = async (e) => {
//     e.preventDefault();

//     try {
//       const formData = new FormData();
//       formData.append("name", name);
//       formData.append("barcode", barcode);
//       formData.append("purchasePrice", purchasePrice);
//       formData.append("salePrice", salePrice);
//       formData.append("stockQuantity", stockQuantity);
//       formData.append("minStockLevel", minStockLevel);
//       formData.append("categoryId", categoryId);
//       formData.append("isActive", isActive);

//       if (image) formData.append("image", image);

//       if (editData) {
//         await api.put(`/product/${editData.id}`, formData, {
//           headers: { "Content-Type": "multipart/form-data" },
//         });
//       } else {
//         await api.post("/product", formData, {
//           headers: { "Content-Type": "multipart/form-data" },
//         });
//       }

//       onSuccess();
//       onClose();
//       resetForm();
//     } catch (err) {
//       console.log(err);
//       alert("Something went wrong");
//     }
//   };

//   if (!show) return null;

//   return (
//     <div className="modal-overlay">
//       <div className="modal-box modern">

//         <div className="modal-header">
//           <h2>{editData ? "✏️ Edit Product" : "➕ Add Product"}</h2>
//           <button onClick={onClose} className="close-x">✕</button>
//         </div>

//         <form onSubmit={handleSubmit} className="modal-grid">

//           <input placeholder="Product Name" value={name}
//             onChange={(e) => setName(e.target.value)} />

//           <input placeholder="Barcode" value={barcode}
//             onChange={(e) => setBarcode(e.target.value)} />

//           <select value={categoryId}
//             onChange={(e) => setCategoryId(e.target.value)}>

//             <option>Select Category</option>
//             {categories.map(c => (
//               <option key={c.id} value={c.id}>{c.name}</option>
//             ))}
//           </select>

//           <input type="number" placeholder="Purchase Price"
//             value={purchasePrice}
//             onChange={(e) => setPurchasePrice(e.target.value)} />

//           <input type="number" placeholder="Sale Price"
//             value={salePrice}
//             onChange={(e) => setSalePrice(e.target.value)} />

//           <input type="number" placeholder="Stock"
//             value={stockQuantity}
//             onChange={(e) => setStockQuantity(e.target.value)} />

//           <input type="number" placeholder="Min Stock"
//             value={minStockLevel}
//             onChange={(e) => setMinStockLevel(e.target.value)} />

//           <div className="file-box">
//             <input type="file"
//               onChange={(e) => setImage(e.target.files[0])} />

//             {editData?.imagePath && (
//               <img
//                 src={`https://localhost:7154${editData.imagePath}`}
//                 className="preview-img"
//               />
//             )}
//           </div>

//           <div className="modal-actions">

//             <button className="btn-primary" type="submit">
//               {editData ? "Update" : "Save"}
//             </button>

//             <button className="btn-danger" type="button" onClick={onClose}>
//               Cancel
//             </button>

//           </div>

//         </form>

//       </div>
//     </div>
//   );
// }
import { useState, useEffect } from "react";
import api, { API_BASE_URL } from "../api/axios";
import "../styles/productForm.css";

export default function ProductForm({
  show,
  onClose,
  onSuccess,
  editData,
}) {
  const [form, setForm] = useState({
    name: "",
    barcode: "",
    purchasePrice: "",
    salePrice: "",
    stockQuantity: "",
    minStockLevel: "",
    categoryId: "",
    isActive: true,
  });

  const [image, setImage] = useState(null);
  const [categories, setCategories] = useState([]);

  useEffect(() => {
    api.get("/category").then((res) => setCategories(res.data));
  }, []);

  useEffect(() => {
    if (editData) {
      setForm({
        name: editData.name || "",
        barcode: editData.barcode || "",
        purchasePrice: editData.purchasePrice || "",
        salePrice: editData.salePrice || "",
        stockQuantity: editData.stockQuantity || "",
        minStockLevel: editData.minStockLevel || "",
        categoryId: editData.categoryId || "",
        isActive: editData.isActive ?? true,
      });
    } else {
      resetForm();
    }
  }, [editData, show]);

  const resetForm = () => {
    setForm({
      name: "",
      barcode: "",
      purchasePrice: "",
      salePrice: "",
      stockQuantity: "",
      minStockLevel: "",
      categoryId: "",
      isActive: true,
    });
    setImage(null);
  };

  const handleChange = (e) => {
    setForm({ ...form, [e.target.name]: e.target.value });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();

    try {
      const fd = new FormData();

      Object.keys(form).forEach((key) => {
        fd.append(key, form[key]);
      });

      if (image) fd.append("image", image);

      if (editData) {
        await api.put(`/product/${editData.id}`, fd);
      } else {
        await api.post("/product", fd);
      }

      onSuccess();
      onClose();
      resetForm();
    } catch (err) {
      console.log(err?.response?.data || err);
      alert("Something went wrong");
    }
  };

  if (!show) return null;

  return (
    <div className="modal-overlay">
      <div className="modal-box modern">

        <div className="modal-header">
          <h2>{editData ? "✏️ Edit Product" : "➕ Add Product"}</h2>
          <button onClick={onClose} className="close-x">✕</button>
        </div>

        <form onSubmit={handleSubmit} className="modal-grid">

          <input name="name" placeholder="Product Name"
            value={form.name} onChange={handleChange} />

          <input name="barcode" placeholder="Barcode"
            value={form.barcode} onChange={handleChange} />

          <select name="categoryId"
            value={form.categoryId}
            onChange={handleChange}>

            <option value="">Select Category</option>
            {categories.map(c => (
              <option key={c.id} value={c.id}>{c.name}</option>
            ))}
          </select>

          <input name="purchasePrice" type="number"
            placeholder="Purchase Price"
            value={form.purchasePrice} onChange={handleChange} />

          <input name="salePrice" type="number"
            placeholder="Sale Price"
            value={form.salePrice} onChange={handleChange} />

          <input name="stockQuantity" type="number"
            placeholder="Stock"
            value={form.stockQuantity} onChange={handleChange} />

          <input name="minStockLevel" type="number"
            placeholder="Min Stock"
            value={form.minStockLevel} onChange={handleChange} />

          <div className="file-box">
            <input type="file"
              onChange={(e) => setImage(e.target.files[0])} />

            {editData?.imagePath && (
              <img
                src={`${API_BASE_URL}${editData.imagePath}`}
                className="preview-img"
                alt="preview"
              />
            )}
          </div>

          <div className="modal-actions">
            <button className="btn-primary" type="submit">
              {editData ? "Update" : "Save"}
            </button>

            <button className="btn-danger" type="button" onClick={onClose}>
              Cancel
            </button>
          </div>

        </form>

      </div>
    </div>
  );
}
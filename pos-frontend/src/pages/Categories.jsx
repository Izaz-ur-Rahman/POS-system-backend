// import { useEffect, useState } from "react";
// import MainLayout from "../layouts/MainLayout";
// import api from "../api/axios";

// import "../styles/pages/category.css";

// export default function Categories() {
//   const [categories, setCategories] = useState([]);
//   const [name, setName] = useState("");
//   const [editId, setEditId] = useState(null);

//   useEffect(() => {
//     loadCategories();
//   }, []);

//   const loadCategories = async () => {
//     const res = await api.get("/category");
//     setCategories(res.data);
//   };

//   const handleSubmit = async (e) => {
//     e.preventDefault();

//     if (editId) {
//       await api.put(`/category/${editId}`, { name });
//     } else {
//       await api.post("/category", { name });
//     }

//     setName("");
//     setEditId(null);
//     loadCategories();
//   };

//   const handleEdit = (cat) => {
//     setName(cat.name);
//     setEditId(cat.id);
//   };

//   const handleDelete = async (id) => {
//     await api.delete(`/category/${id}`);
//     loadCategories();
//   };

//   return (
//     <MainLayout>

//       {/* HEADER */}
//       <div className="page-header">
//         <div className="page-title">
//           <h2>Categories</h2>
//           <p>Manage product categories</p>
//         </div>
//       </div>

//       {/* FORM */}
//       <form className="category-form" onSubmit={handleSubmit}>
//         <input
//           type="text"
//           placeholder="Category name..."
//           value={name}
//           onChange={(e) => setName(e.target.value)}
//         />

//         <button type="submit">
//           {editId ? "Update" : "Add"}
//         </button>
//       </form>

//       {/* TABLE */}
//       <table className="category-table">
//         <thead>
//           <tr>
//             <th>ID</th>
//             <th>Name</th>
//             <th>Actions</th>
//           </tr>
//         </thead>

//         <tbody>
//           {categories.map((c) => (
//             <tr key={c.id}>
//               <td>{c.id}</td>
//               <td>{c.name}</td>
//               <td>
//                 <button onClick={() => handleEdit(c)}>Edit</button>
//                 <button onClick={() => handleDelete(c.id)}>Delete</button>
//               </td>
//             </tr>
//           ))}
//         </tbody>
//       </table>

//     </MainLayout>
//   );
// }
import { useEffect, useState } from "react";
import MainLayout from "../layouts/MainLayout";
import api from "../api/axios";

import "../styles/pages/category.css";
import "../styles/pages/categoryModal.css";

export default function Categories() {
  const [categories, setCategories] = useState([]);

  const [showModal, setShowModal] = useState(false);
    const [search, setSearch] = useState("");

  const [name, setName] = useState("");

  const [editId, setEditId] = useState(null);
const [deleteItem, setDeleteItem] = useState(null);

  useEffect(() => {
    loadCategories();
  }, []);

  const loadCategories = async () => {
    const res = await api.get("/category");
    setCategories(res.data);
  };

  const handleSubmit = async (e) => {
    e.preventDefault();

    if (editId) {
      await api.put(`/category/${editId}`, {
        name,
      });
    } else {
      await api.post("/category", {
        name,
      });
    }

    setName("");
    setEditId(null);
    setShowModal(false);

    loadCategories();
  };

  const handleEdit = (cat) => {
    setEditId(cat.id);
    setName(cat.name);
    setShowModal(true);
  };

  const handleDelete = async (id) => {
    await api.delete(`/category/${id}`);
    loadCategories();
  };

  return (
    <MainLayout>

      <div className="page-header">

        <div className="page-title">
          <h2>Categories</h2>
          <p>Manage product categories</p>
        </div>
  <div className="search-box">
                <input
                    placeholder="Search Category..."
                    value={search}
                    onChange={(e) => setSearch(e.target.value)}
                />
            </div>
        <button
          className="add-category-btn"
          onClick={() => {
            setEditId(null);
            setName("");
            setShowModal(true);
          }}
        >
          ➕ Add Category
        </button>

      </div>

      <table className="category-table">

        <thead>
          <tr>
          
            <th>Category Name</th>
              <th>Status</th>
            <th>Actions</th>
          </tr>
        </thead>

        <tbody>

          {categories
            .filter(s =>
                            s.name?.toLowerCase().includes(search.toLowerCase())
                        )
          .map((c) => (

            <tr key={c.id}>

             

              <td>{c.name}</td>
               <td>
                 <span className={c.isActive ? "active" : "inactive"}>
                                        {c.isActive ? "Active" : "Inactive"}
                                    </span>
               </td>

              <td>

<div className="category-actions">

<button
className="action-btn edit-btn"
onClick={() => handleEdit(c)}
>
✏
</button>

<button
className="action-btn delete-btn"
onClick={() => setDeleteItem(c)}
>
🗑
</button>

</div>

</td>
            </tr>

          ))}

        </tbody>

      </table>

      {showModal && (

        <div className="category-modal-overlay">

          <div className="category-modal">

            <div className="category-modal-header">

              <h2>
                {editId
                  ? "✏ Edit Category"
                  : "➕ Add Category"}
              </h2>

              <button
                className="close-category-modal"
                onClick={() => setShowModal(false)}
              >
                ✕
              </button>

            </div>

            <form onSubmit={handleSubmit}>

              <input
                type="text"
                placeholder="Enter category name"
                value={name}
                onChange={(e) =>
                  setName(e.target.value)
                }
                required
              />

              <div className="category-modal-actions">

                <button
                  className="save-category-btn"
                  type="submit"
                >
                  {editId
                    ? "Update Category"
                    : "Save Category"}
                </button>

                <button
                  className="cancel-category-btn"
                  type="button"
                  onClick={() => setShowModal(false)}
                >
                  Cancel
                </button>

              </div>

            </form>

          </div>

        </div>

      )}
{deleteItem && (

<div className="delete-overlay">

<div className="delete-modal">

<div className="delete-warning">
⚠️
</div>

<h2>Delete Category?</h2>

<p>

Are you sure you want to delete

<br/>

<b>{deleteItem.name}</b>

</p>

<div className="delete-modal-actions">

<button

className="cancel-delete"

onClick={() => setDeleteItem(null)}

>

Cancel

</button>

<button

className="confirm-delete"

onClick={async () => {

await handleDelete(deleteItem.id);

setDeleteItem(null);

}}

>

Delete

</button>

</div>

</div>

</div>

)}
    </MainLayout>
  );
}
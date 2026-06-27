import { useEffect, useState } from "react";
import api from "../api/axios";
//import ThermalInvoice from "./invoice/ThermalInvoice";
export default function SaleDetails({ id, onClose }) {
const downloadPdf = async () => {
    try {

        const response = await api.get(
            `/invoice/${id}/pdf`,
            {
                responseType: "blob"
            }
        );

        const url = window.URL.createObjectURL(
            new Blob([response.data], {
                type: "application/pdf"
            })
        );

        const link = document.createElement("a");

        link.href = url;
        link.setAttribute(
            "download",
            `Invoice-${id}.pdf`
        );

        document.body.appendChild(link);

        link.click();

        link.remove();

        window.URL.revokeObjectURL(url);

    } catch (err) {
        console.log(err);
    }
};
const printInvoice = async () => {
    try {
        const response = await api.get(
            `/invoice/${id}/pdf`,
            {
                responseType: "blob"
            }
        );

        const url = window.URL.createObjectURL(
            new Blob([response.data], {
                type: "application/pdf"
            })
        );

        const printWindow = window.open(url);

        printWindow.onload = () => {
            printWindow.print();
        };

    } catch (err) {
        console.log(err);
    }
};
// const printInvoice = async () => {

//     const res = await api.get(
//         `/invoice/${id}/print`
//     );

//     const w = window.open("", "_blank");

//     w.document.write(`
//         <html>
//         <body>

//         <h2>${res.data.store}</h2>

//         <hr>

//         <p>
//         Invoice:
//         ${res.data.invoiceNo}
//         </p>

//         <p>
//         Customer:
//         ${res.data.customerName}
//         </p>

//         <p>
//         Date:
//         ${new Date(res.data.date)
//             .toLocaleString()}
//         </p>

//         <hr>

//         ${res.data.items.map(x => `
//             <p>
//             ${x.productName}
//             -
//             ${x.quantity}
//             x
//             ${x.price}
//             =
//             ${x.subTotal}
//             </p>
//         `).join("")}

//         <hr>

//         <h3>
//         Total :
//         ${res.data.totalAmount}
//         </h3>

//         </body>
//         </html>
//     `);

//     w.document.close();

//     w.print();
// };
// const printInvoice = async () => {

//     const res = await api.get(`/invoice/${id}/print`);

//     const data = res.data;

//     const win = window.open("", "", "width=700,height=800");

//     win.document.write(`

//         <html>

//         <body>

//         <h2>${data.store}</h2>

//         <hr/>

//         <h3>Invoice : ${data.invoiceNo}</h3>

//         <p>Customer : ${data.customerName}</p>

//         <p>Date : ${new Date(data.date).toLocaleString()}</p>

//         <hr/>

//         ${data.items.map(x=>`
//             <p>
//             ${x.productName}
//             (${x.quantity})
//             x
//             ${x.price}
//             </p>
//         `).join("")}

//         <hr/>

//         <h2>Total : ${data.totalAmount}</h2>

//         </body>

//         </html>

//     `);

//     win.document.close();

//     win.print();
// };
    const [data, setData] = useState(null);

    useEffect(() => {
        load();
    }, []);

    const load = async () => {
        const res = await api.get(`/sale/${id}`);
        setData(res.data);
    };

    if (!data) return null;

    return (
        <div className="modal-overlay">

            <div className="modal-box">

                <h2>Invoice: {data.invoiceNo}</h2>

                <p>Customer: {data.customerName}</p>
                <p>Date: {new Date(data.saleDate).toLocaleString()}</p>

                <hr />

                {data.items.map((item, i) => (
                    <p key={i}>
                        {item.productName} : {item.quantity} x {item.salePrice}
                    </p>
                ))}

                <hr />

                <h3>Total: {data.totalAmount}</h3>

                {/* <button onClick={onClose}>Close</button> */}
<div className="invoice-buttons">

    <button
        className="pdf-btn"
        onClick={downloadPdf}
    >
        📄 PDF
    </button>

    <button
    className="print-btn"
    onClick={printInvoice}
>
    Print
</button>

    {/* <ThermalInvoice saleId={id} /> */}

    <button
        className="close-btn"
        onClick={onClose}
    >
        ❌ Close
    </button>

</div>
            </div>

        </div>
    );
}
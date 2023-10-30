import { useEffect, useState } from "react";
import "./App.css";
import { routes } from "./config";
import { Currency } from "./Currency";

interface Product {
  ProductId: string;
}

function App() {
  const [currency, setCurrency] = useState(Currency.AUD);
  const [data, setData] = useState();
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState();

  useEffect(() => {
    fetch(routes.GET_PRODUCTS, {
      method: "GET",
    })
      .then((res) => res.json())
      .then((data) => console.log(data));

    fetch(routes.GET_FX_RATES, {
      method: "GET",
    })
      .then((res) => res.json())
      .then((data) => console.log(data));
  }, []);

  return (
    <div>
      <header className="header default-bg-color header-color">
        <p>CityFM</p>
        <button>{currency}</button>
      </header>
      <body>
        <p>Body stuff stuff</p>
      </body>
    </div>
  );
}

export default App;

import { useEffect, useState } from "react";
import "./App.css";
import { routes } from "./config";

function App() {
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
      <header className="header default-bg-color s">
        <p className="header-color">CityFM</p>
      </header>
      <body>
        <p>Body stuff stuff</p>
      </body>
    </div>
  );
}

export default App;

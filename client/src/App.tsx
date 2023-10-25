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
    });
  }, []);

  return (
    <div>
      <header className="header default-bg-color">
        <p className="header-color">CityFM</p>
      </header>
      <body>
        <p>Body stuff stuff</p>
      </body>
    </div>
  );
}

export default App;

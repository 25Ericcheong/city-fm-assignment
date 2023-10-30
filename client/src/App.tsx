import { useEffect, useState } from "react";
import "./App.css";
import { routes } from "./config";
import { Currency } from "./Currency";

interface Product {
  Description: string;
  ProductId: string;
  MaximumQuantity: number | undefined;
  Name: string;
  UnitPrice: number;
}

interface FxRate {
  Rate: number;
  SourceCurrency: Currency;
  TargetCurrency: Currency;
}

function App() {
  const [currentRate, setCurrentRate] = useState(Currency.AUD);
  const [rateMultipler, setRateMultipler] = useState(1);
  const [rateData, setRateData] = useState<FxRate[]>();
  const [rateDataLoading, setRateDataLoading] = useState(false);
  const [productData, setProductData] = useState<Product[]>([]);
  const [productDataLoading, setProductDataLoading] = useState(false);
  const [error, setError] = useState(undefined);
  const [rateError, setRateError] = useState(undefined);

  useEffect(() => {
    setProductDataLoading(true);
    getProductData();

    setRateDataLoading(true);
    getFxRateData();
  }, []);

  function getProductData() {
    fetch(routes.GET_PRODUCTS, {
      method: "GET",
    })
      .then((res) => res.json())
      .then((data: Product[]) => {
        setProductData(data);
        setProductDataLoading(false);
      })
      .finally(() => {
        setProductDataLoading(false);
      })
      .catch((e) => setError(e));
  }

  function getSpecificRate(
    rates: FxRate[],
    sourceRate: Currency,
    targetRate: Currency
  ) {
    return rateData?.find(
      (r) => r.SourceCurrency === sourceRate && r.TargetCurrency === targetRate
    );
  }

  function processFxRateData(respond: FxRate[]): FxRate[] {
    const processed = [...respond];

    processed.push({
      SourceCurrency: Currency.GBP,
      TargetCurrency: Currency.USD,
      Rate:
        (getSpecificRate(respond, Currency.GBP, Currency.AUD)?.Rate ?? 1) *
        (getSpecificRate(respond, Currency.AUD, Currency.USD)?.Rate ?? 1),
    });

    processed.push({
      SourceCurrency: Currency.USD,
      TargetCurrency: Currency.GBP,
      Rate:
        (getSpecificRate(respond, Currency.USD, Currency.AUD)?.Rate ?? 1) *
        (getSpecificRate(respond, Currency.AUD, Currency.GBP)?.Rate ?? 1),
    });

    return processed;
  }

  function getFxRateData() {
    fetch(routes.GET_FX_RATES, {
      method: "GET",
    })
      .then((res) => res.json())
      .then((data: FxRate[]) => {
        setRateData(processFxRateData(data));
        setRateDataLoading(false);
      })
      .finally(() => {
        setRateDataLoading(false);
      })
      .catch((e) => setRateError(e));
  }

  function renderProductData() {
    if (error) {
      <div>
        <p>
          An error has occurred while trying to acquire product data. Please
          retry.
        </p>
      </div>;
    }

    if (productDataLoading) {
      return (
        <div>
          <p>Product data is loading...</p>
        </div>
      );
    }

    if (productData.length === 0) {
      return (
        <div className="error">
          <p>Product data is missing. Please retry.</p>
          <button onClick={() => getProductData()}>Retry</button>
        </div>
      );
    }

    return productData.map((p) => {
      return (
        <div>
          <h2>{p.Name}</h2>
          <p>Description: {p.Description}</p>
          <p>Maximum quantity: {p.MaximumQuantity ?? "Not specified"}</p>
          <p>Price: {`${p.UnitPrice * rateMultipler} ${currentRate}`}</p>
        </div>
      );
    });
  }

  function renderRateOptions() {
    if (rateError) {
      return (
        <p>Error found while trying to load rate data. Please try again</p>
      );
    }

    if (rateDataLoading) {
      return <p>Rate data is loading...</p>;
    }

    const options = [...new Set(rateData?.map((r) => r.SourceCurrency))];
    return options
      .filter((rate) => rate !== currentRate)
      .map((rate: Currency) => {
        return <button onClick={() => handleRateUpdate(rate)}>{rate}</button>;
      });
  }

  function handleRateUpdate(targetRate: Currency) {
    const sourceRate = currentRate;
    const rate = rateData?.find(
      (r) => r.SourceCurrency === sourceRate && r.TargetCurrency === targetRate
    );

    if (rate === undefined) {
    }

    if (rate === undefined) {
      throw Error("Rate data is not found. Please reload browser.");
    }

    setRateMultipler(rate.Rate);
    setCurrentRate(targetRate);
  }

  return (
    <div>
      <header className="header default-bg-color header-color">
        <p>CityFM</p>
        <div>
          <button>{currentRate}</button>
        </div>
      </header>
      <body>
        <section className="product">
          <h1>Products Available</h1>
          {renderProductData()}
        </section>
      </body>
      <footer className="footer default-bg-color header-color">
        <p>Change currency to:</p>
        <div>{renderRateOptions()}</div>
      </footer>
    </div>
  );
}

export default App;

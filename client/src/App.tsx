import React from 'react';
import { createMuiTheme, ThemeProvider } from '@material-ui/core/styles';
import { Route, BrowserRouter } from 'react-router-dom';

import './App.css';
import CategoryPage from './Components/Pages/CategoryPage';
import ProductsPage from './Components/Pages/ProductsPage';
import ProductPage from './Components/Pages/ProductPage';
import RegistrationPage from './Components/Pages/RegistrationPage';

const innerTheme = createMuiTheme({
  palette: {
    primary: {
      main: '#ef6c00',
    },
    secondary: {
      main: '#FFF',
    },
  },
});

const App: React.FC = () => (
  <div className="App">
    <ThemeProvider theme={innerTheme}>
      <BrowserRouter>
        <div>
          <Route exact path="/" component={ProductsPage} />
          <Route path="/category/:categoryName" component={CategoryPage} />
          <Route path="/product/:productID" component={ProductPage} />
          <Route path="/registration" component={RegistrationPage} />
        </div>
      </BrowserRouter>
    </ThemeProvider>
  </div>
);

export default App;

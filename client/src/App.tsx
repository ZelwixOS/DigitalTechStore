import React from 'react';
import { createTheme, ThemeProvider } from '@material-ui/core/styles';
import { Route, Routes } from 'react-router-dom';

import './App.css';
import CategoryPage from './Components/Pages/CategoryPage';
import ProductsPage from './Components/Pages/ProductsPage';
import ProductPage from './Components/Pages/ProductPage';
import RegistrationPage from './Components/Pages/RegistrationPage';
import CartPage from './Components/Pages/CartPage';
import WishlistPage from './Components/Pages/WishlistPage';
import CommonCategoryPage from './Components/Pages/CommonCategoryPage';
import PurchasingPage from './Components/Pages/PurchasingPage';
import UserPurchases from './Components/Pages/UserPurchases';
import { CommonCategoryList } from './Components/Admin/Pages/CommonCategoryList';
import { CategoryList } from './Components/Admin/Pages/CategoryList';
import { AdminPage } from './Components/Admin/Pages/AdminPage';

const innerTheme = createTheme({
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
  <ThemeProvider theme={innerTheme}>
    <Routes>
      <Route path="/" element={<ProductsPage />} />
      <Route path="/common/:commonCategoryName" element={<CommonCategoryPage />} />
      <Route path="/category/:categoryName" element={<CategoryPage />} />
      <Route path="/cart" element={<CartPage />} />
      <Route path="/wishlist" element={<WishlistPage />} />
      <Route path="/product/:productID" element={<ProductPage />} />
      <Route path="/registration" element={<RegistrationPage />} />
      <Route path="/purchasing" element={<PurchasingPage />} />
      <Route path="/purchases" element={<UserPurchases />} />
      <Route path="/admin" element={<AdminPage />} />
      <Route path="/admin/commonCategories" element={<CommonCategoryList />} />
      <Route path="/admin/categories" element={<CategoryList />} />
    </Routes>
  </ThemeProvider>
);

export default App;

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
import { ParameterBlockList } from './Components/Admin/Pages/ParameterBlockList';
import { TechParameterList } from './Components/Admin/Pages/TechParameterList';
import { WorkersPage } from './Components/Admin/Pages/WorkersPage';
import { ProductParameterList } from './Components/Admin/Pages/ProductParameterList';
import { ProductValueList } from './Components/Admin/Parts/ProductValueList';
import { RegionList } from './Components/Admin/Pages/RegionList';
import { OutletList } from './Components/Admin/Pages/OutletList';
import { WarehouseList } from './Components/Admin/Pages/WarehouseList';
import { ClientsList } from './Components/Admin/Pages/ClientsList';
import OutletOrdersPage from './Components/Pages/OutletOrdersPage';
import HistoricalOrdersPage from './Components/Pages/HistoricalOrdersPage';
import Footer from './Components/Parts/Footer';
import Shops from './Components/Pages/Shops';

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
      <Route path="/admin/parameterBlocks" element={<ParameterBlockList />} />
      <Route path="/admin/parameters" element={<TechParameterList />} />
      <Route path="/admin/workers" element={<WorkersPage />} />
      <Route path="/admin/productParameters" element={<ProductParameterList />} />
      <Route path="/admin/product" element={<ProductValueList />} />
      <Route path="/admin/regions" element={<RegionList />} />
      <Route path="/admin/outlets" element={<OutletList />} />
      <Route path="/admin/warehouses" element={<WarehouseList />} />
      <Route path="/admin/Clients" element={<ClientsList />} />
      <Route path="/OutletOrders" element={<OutletOrdersPage />} />
      <Route path="/OrdersHistory" element={<HistoricalOrdersPage />} />
      <Route path="/Shops" element={<Shops />} />
    </Routes>
    <Footer />
  </ThemeProvider>
);

export default App;

import React from 'react';
import Drawer from '@material-ui/core/Drawer';
import List from '@material-ui/core/List';
import Divider from '@material-ui/core/Divider';
import ListItem from '@material-ui/core/ListItem';
import ListItemText from '@material-ui/core/ListItemText';
import MenuIcon from '@material-ui/icons/Menu';
import { IconButton, MenuItem, Typography } from '@material-ui/core';

import Roles from 'src/Types/Roles';
import { getRole } from 'src/Requests/AccountRequests';

const HiddenNavigation = () => {
  const [state, setState] = React.useState(false);
  const [role, setRole] = React.useState('');

  const toggleDrawer = (open: boolean) => (event: React.KeyboardEvent | React.MouseEvent) => {
    if (
      event.type === 'keydown' &&
      ((event as React.KeyboardEvent).key === 'Tab' || (event as React.KeyboardEvent).key === 'Shift')
    ) {
      return;
    }

    setState(open);
  };

  const onMenuItemClick = (loc: string) => {
    window.location.href = loc;
  };

  const categories = onMenuItemClick.bind(this, '/admin/categories');
  const commonCategories = onMenuItemClick.bind(this, '/admin/commonCategories');
  const parameterBlocks = onMenuItemClick.bind(this, '/admin/parameterBlocks');
  const parameters = onMenuItemClick.bind(this, '/admin/parameters');
  const workers = onMenuItemClick.bind(this, '/admin/workers');
  const clients = onMenuItemClick.bind(this, '/admin/clients');
  const products = onMenuItemClick.bind(this, '/admin/product');
  const regions = onMenuItemClick.bind(this, '/admin/regions');
  const outlets = onMenuItemClick.bind(this, '/admin/outlets');
  const warehouses = onMenuItemClick.bind(this, '/admin/warehouses');

  const menuItem = (name: string, click: () => void) => (
    <MenuItem onClick={click}>
      <ListItem>
        <ListItemText>
          <Typography variant="h6">{name}</Typography>
        </ListItemText>
      </ListItem>
    </MenuItem>
  );

  React.useEffect(() => {
    let isMounted = true;
    checkAuth(isMounted);
    return () => {
      isMounted = false;
    };
  });

  const checkAuth = async (isMounted: boolean) => {
    const authres = await getRole();
    setRole(authres);
    if (isMounted) {
      if (authres !== Roles.guest) {
        sessionStorage.setItem('signed', authres);
      }
    }
  };

  return (
    <div>
      <IconButton color="secondary" aria-label="menu" onClick={toggleDrawer(true)}>
        <MenuIcon />
      </IconButton>
      <Drawer anchor={'left'} open={state} onClose={toggleDrawer(false)}>
        {role === 'Admin' && (
          <List>
            {menuItem('???????????????????? ??????????????????', commonCategories)}
            <Divider />
            {menuItem('??????????????????', categories)}
            <Divider />
            {menuItem('?????????? ????????????????????', parameterBlocks)}
            <Divider />
            {menuItem('??????????????????', parameters)}
            <Divider />
            {menuItem('????????????????', products)}
            <Divider />
            {menuItem('??????????????????', workers)}
            <Divider />
            {menuItem('??????????????', clients)}
            <Divider />
            {menuItem('??????????????', regions)}
            <Divider />
            {menuItem('????????????????', outlets)}
            <Divider />
            {menuItem('????????????', warehouses)}
          </List>
        )}
        {role === 'Manager' && (
          <List>
            {menuItem('??????????????????', workers)}
            <Divider />
            {menuItem('??????????????', clients)}
            <Divider />
            {menuItem('????????????????', products)}
          </List>
        )}
      </Drawer>
    </div>
  );
};

export default HiddenNavigation;

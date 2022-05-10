import React from 'react';
import { makeStyles } from '@material-ui/core/styles';
import Drawer from '@material-ui/core/Drawer';
import Button from '@material-ui/core/Button';
import List from '@material-ui/core/List';
import Divider from '@material-ui/core/Divider';
import ListItem from '@material-ui/core/ListItem';
import ListItemText from '@material-ui/core/ListItemText';
import MenuIcon from '@material-ui/icons/Menu';
import { IconButton, MenuItem, Typography } from '@material-ui/core';

const useStyles = makeStyles({
  list: {
    width: 250,
  },
  fullList: {
    width: 'auto',
  },
});

const HiddenNavigation = () => {
  const classes = useStyles();
  const [state, setState] = React.useState(false);

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

  return (
    <div>
      <IconButton color="secondary" aria-label="menu" onClick={toggleDrawer(true)}>
        <MenuIcon />
      </IconButton>
      <Drawer anchor={'left'} open={state} onClose={toggleDrawer(false)}>
        <List>
          <MenuItem onClick={commonCategories}>
            <ListItem>
              <ListItemText>
                <Typography variant="h6">Обобщающие категории</Typography>
              </ListItemText>
            </ListItem>
          </MenuItem>
          <Divider />
          <MenuItem onClick={categories}>
            <ListItem>
              <ListItemText>
                <Typography variant="h6">Категории</Typography>
              </ListItemText>
            </ListItem>
          </MenuItem>
        </List>
      </Drawer>
    </div>
  );
};

export default HiddenNavigation;

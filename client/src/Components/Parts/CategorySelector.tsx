import React from 'react';
import Button from '@material-ui/core/Button';
import ClickAwayListener from '@material-ui/core/ClickAwayListener';
import Paper from '@material-ui/core/Paper';
import Popper from '@material-ui/core/Popper';
import MenuItem from '@material-ui/core/MenuItem';
import MenuList from '@material-ui/core/MenuList';
import { makeStyles, createStyles, Theme } from '@material-ui/core/styles';
import ArrowDropDownIcon from '@material-ui/icons/ArrowDropDown';
import ArrowDropUpIcon from '@material-ui/icons/ArrowDropUp';
import { Typography } from '@material-ui/core';

import Category from 'src/Types/Category';
import { getCategories } from 'src/Requests/GetRequests';

const useStyles = makeStyles((theme: Theme) =>
  createStyles({
    root: {
      display: 'flex',
    },
    paper: {
      marginRight: theme.spacing(2),
    },
    menu: {
      padding: theme.spacing(1),
    },
    item: {
      paddingRight: theme.spacing(4),
      paddingLeft: theme.spacing(4),
      paddingTop: theme.spacing(2),
      paddingBottom: theme.spacing(2),
    },
  }),
);

const CategorySelector: React.FC = () => {
  const classes = useStyles();

  const [open, setOpen] = React.useState<boolean>(false);
  const [categories, setCategories] = React.useState<Category[]>([]);

  const anchorRef = React.useRef<HTMLButtonElement>(null);

  const handleToggle = () => {
    setOpen(prevOpen => !prevOpen);
  };

  const handleClose = (event: React.MouseEvent<EventTarget>) => {
    if (anchorRef.current && anchorRef.current.contains(event.target as HTMLElement)) {
      return;
    }

    setOpen(false);
  };

  function handleListKeyDown(event: React.KeyboardEvent) {
    const keyValue = 'Tab';
    if (event.key === keyValue) {
      event.preventDefault();
      setOpen(false);
    }
  }

  const prevOpen = React.useRef(open);
  React.useEffect(() => {
    if (prevOpen.current === true && open === false && anchorRef.current !== null) {
      anchorRef.current.focus();
    }

    prevOpen.current = open;
  }, [open]);

  React.useEffect(() => {
    let isMounted = true;
    const getCategs = async () => {
      const res = await getCategories();
      if (isMounted) {
        setCategories(res);
      }
    };
    getCategs();

    return () => {
      isMounted = false;
    };
  }, []);

  return (
    <div className={classes.root}>
      {open ? (
        <Button
          variant="outlined"
          size="large"
          color="secondary"
          ref={anchorRef}
          aria-controls={open ? 'menu-list-grow' : undefined}
          aria-haspopup="true"
          onClick={handleToggle}
          endIcon={<ArrowDropUpIcon />}
        >
          Categories
        </Button>
      ) : (
        <Button
          variant="outlined"
          size="large"
          color="secondary"
          ref={anchorRef}
          aria-controls={open ? 'menu-list-grow' : undefined}
          aria-haspopup="true"
          onClick={handleToggle}
          endIcon={<ArrowDropDownIcon />}
        >
          Categories
        </Button>
      )}
      <Popper open={open} anchorEl={anchorRef.current} role={undefined} transition disablePortal>
        <Paper>
          <ClickAwayListener onClickAway={handleClose}>
            <MenuList autoFocusItem={open} id="menu-list-grow" onKeyDown={handleListKeyDown} className={classes.menu}>
              {categories instanceof Array &&
                categories.map((category, index) => (
                  <MenuItem
                    className={classes.item}
                    key={index}
                    onClick={() => {
                      document.location.href = `/category/${category.name}`;
                    }}
                  >
                    <Typography variant="h6" component="h6">
                      {category.name}
                    </Typography>
                  </MenuItem>
                ))}
            </MenuList>
          </ClickAwayListener>
        </Paper>
      </Popper>
    </div>
  );
};

export default CategorySelector;

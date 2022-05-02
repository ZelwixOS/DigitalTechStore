import React, { useEffect, useState, useRef } from 'react';
import { makeStyles, createStyles, Theme } from '@material-ui/core/styles';
import Button from '@material-ui/core/Button';
import ClickAwayListener from '@material-ui/core/ClickAwayListener';
import Paper from '@material-ui/core/Paper';
import Popper from '@material-ui/core/Popper';
import MenuItem from '@material-ui/core/MenuItem';
import MenuList from '@material-ui/core/MenuList';
import { Divider, Typography } from '@material-ui/core';

import { getUserInfo, logOut } from 'src/Requests/AccountRequests';
import UserInfo from 'src/Types/UserMainInfo';

import UserAvatar from './UserAvatar';

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

const UserMiniPanel: React.FC = () => {
  const classes = useStyles();

  const [userInfo, setUserInfo] = useState<UserInfo>();
  const [open, setOpen] = React.useState<boolean>(false);
  const anchorRef = React.useRef<HTMLButtonElement>(null);

  const handleToggle = () => {
    setOpen(prevOpen => !prevOpen);
  };

  const prevOpen = useRef(open);
  useEffect(() => {
    if (prevOpen.current === true && open === false && anchorRef.current !== null) {
      anchorRef.current.focus();
    }

    prevOpen.current = open;
  }, [open]);

  useEffect(() => {
    let isMounted = true;
    getUserInformation(isMounted);
    return () => {
      isMounted = false;
    };
  }, []);

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

  const getUserInformation = async (isMounted: boolean) => {
    const userInformation = await getUserInfo();

    if (isMounted) {
      setUserInfo(userInformation);
    }
  };

  const signOut = async () => {
    await logOut();
    sessionStorage.removeItem('signed');
    document.location.href = `/`;
  };

  return (
    <React.Fragment>
      {userInfo && (
        <React.Fragment>
          <Button onClick={handleToggle} ref={anchorRef}>
            <UserAvatar userInfo={{ userName: userInfo.userName, avatar: userInfo.avatar, role: userInfo.role }} />
          </Button>
          <Popper open={open} anchorEl={anchorRef.current} role={undefined} transition disablePortal>
            <Paper>
              <ClickAwayListener onClickAway={handleClose}>
                <MenuList
                  autoFocusItem={open}
                  id="menu-list-grow"
                  onKeyDown={handleListKeyDown}
                  className={classes.menu}
                >
                  <Typography variant="h6" color="primary" component="h6">
                    {userInfo.userName}
                  </Typography>
                  <Typography variant="subtitle2" component="h6">
                    {`${userInfo.firstName} ${userInfo.secondName}`}
                  </Typography>
                  <Divider />
                  <MenuItem
                    className={classes.item}
                    onClick={() => {
                      document.location.href = `/profile`;
                    }}
                  >
                    <Typography variant="h6" component="h6">
                      Профиль
                    </Typography>
                  </MenuItem>
                  <MenuItem
                    className={classes.item}
                    onClick={() => {
                      document.location.href = `/settings`;
                    }}
                  >
                    <Typography variant="h6" component="h6">
                      Настройки
                    </Typography>
                  </MenuItem>
                  <MenuItem className={classes.item} onClick={signOut}>
                    <Typography variant="h6" component="h6">
                      Выйти
                    </Typography>
                  </MenuItem>
                </MenuList>
              </ClickAwayListener>
            </Paper>
          </Popper>
        </React.Fragment>
      )}
    </React.Fragment>
  );
};
export default UserMiniPanel;

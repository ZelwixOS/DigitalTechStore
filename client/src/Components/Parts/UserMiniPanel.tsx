import Avatar from '@material-ui/core/Avatar';
import React, { useEffect, useState } from 'react';

import { getUserInfo } from 'src/Requests/AccountRequests';
import UserInfo from 'src/Types/UserInfo';

const UserMiniPanel: React.FC = () => {
  const [userInfo, setUserInfo] = useState<UserInfo>();
  useEffect(() => {
    let isMounted = true;
    getUserInformation(isMounted);
    return () => {
      isMounted = false;
    };
  });

  const getUserInformation = async (isMounted: boolean) => {
    const userInformation = await getUserInfo();

    if (isMounted) {
      setUserInfo(userInformation);
    }
  };

  function stringToColor(string: string) {
    let hash = 0;
    let i;

    for (i = 0; i < string.length; i += 1) {
      hash = string.charCodeAt(i) + ((hash << 5) - hash);
    }

    let color = '#';

    for (i = 0; i < 3; i += 1) {
      const value = (hash >> (i * 8)) & 0xff;
      color += `00${value.toString(16)}`.substr(-2);
    }

    return color;
  }

  function stringAvatar(name: string) {
    return {
      sx: {
        bgcolor: stringToColor(name),
      },
      children: `${name.split(' ')[0][0]}`,
    };
  }

  return (
    <React.Fragment>
      {userInfo &&
        (userInfo.avatar ? (
          <Avatar alt={userInfo.userName} src={userInfo.avatar} />
        ) : (
          <Avatar {...stringAvatar(userInfo.userName)} />
        ))}
    </React.Fragment>
  );
};
export default UserMiniPanel;

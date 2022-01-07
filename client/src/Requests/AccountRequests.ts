import axios from 'axios';

import Login from 'src/Types/Login';
import Register from 'src/Types/Register';
import ServerResponse from 'src/Types/ServerResponse';
import UserMainInfo from 'src/Types/UserMainInfo';

const serverAnswers = {
  noCommand: 0,
  signedIn: 3,
  userNotFound: 10,
};

const authViaGoogle = async (token: string): Promise<ServerResponse> =>
  (await axios.post('/api/Account/GoogleAuth', { token: token })).data;

const logInRequest = async (logInData: Login): Promise<ServerResponse> =>
  (await axios.post('/api/Account/Login', logInData)).data;

const registrationRequest = async (registrationData: Register): Promise<ServerResponse> =>
  (await axios.post('/api/Account/Register', registrationData)).data;

const regViaGoogleRequest = async (registrationData: Register): Promise<ServerResponse> =>
  (await axios.post('/api/Account/RegisterViaGoogle', registrationData)).data;

const getRole = async (): Promise<string> => (await axios.get('/api/Account/Role')).data;

const getUserInfo = async (): Promise<UserMainInfo> => (await axios.get('/api/Account/GetCurrentUserInfo')).data;

const logOut = async (): Promise<string> => (await axios.post('/api/Account/LogOut')).data;

export {
  serverAnswers,
  authViaGoogle,
  logInRequest,
  registrationRequest,
  regViaGoogleRequest,
  getRole,
  getUserInfo,
  logOut,
};

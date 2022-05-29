import City from './City';
import Region from './Region';

type Outlet = {
  id: number;
  name: string;
  region: Region;
  city: City;
  streetName: string;
  building: string;
  postalCode: string;
  phoneNumber: string;
  userNote: string;
};

export default Outlet;

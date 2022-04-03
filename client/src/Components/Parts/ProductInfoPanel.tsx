import React from 'react';
import { makeStyles, Theme } from '@material-ui/core/styles';
import Tabs from '@material-ui/core/Tabs';
import Tab from '@material-ui/core/Tab';
import Grid from '@material-ui/core/Grid';

import Product from 'src/Types/Product';
import ProductDescription from 'src/Components/Parts/ProductDescription';
import ProductParams from 'src/Components/Parts/ProductParams';
import ProductReviews from 'src/Components/Parts/ProductReviews';

interface TabPanelProps {
  children?: React.ReactNode;
  index: unknown;
  value: unknown;
}

const TabPanel = (props: TabPanelProps) => {
  const { children, value, index, ...other } = props;

  return (
    <div
      role="tabpanel"
      hidden={value !== index}
      id={`vertical-tabpanel-${index}`}
      aria-labelledby={`vertical-tab-${index}`}
      {...other}
    >
      {value === index && children}
    </div>
  );
};

function a11yProps(index: unknown) {
  return {
    id: `vertical-tab-${index}`,
    'aria-controls': `vertical-tabpanel-${index}`,
  };
}

const useStyles = makeStyles((theme: Theme) => ({
  root: {
    backgroundColor: theme.palette.background.paper,
  },
  tabs: {
    borderRight: `1px solid ${theme.palette.divider}`,
  },
}));

interface IVerticalTabs {
  product: Product;
}

const ProductInfoPanel = (props: IVerticalTabs) => {
  const classes = useStyles();
  const [value, setValue] = React.useState(0);

  const handleChange = (event: unknown, newValue: number) => {
    setValue(newValue);
  };

  const saveReviewed = () => {
    props.product.reviewed = true;
  };

  return (
    <div className={classes.root}>
      <Grid container direction="row" justify="center">
        <Grid className={classes.tabs} item xs={12} sm={3}>
          <Tabs
            orientation="vertical"
            variant="scrollable"
            value={value}
            onChange={handleChange}
            aria-label="Vertical tabs"
            textColor="primary"
          >
            <Tab label="Описание" {...a11yProps(0)} />
            <Tab label="Характеристики" {...a11yProps(1)} />
            <Tab label="Отзывы" {...a11yProps(2)} />
          </Tabs>
        </Grid>
        <Grid item xs={12} sm={9}>
          <TabPanel value={value} index={0}>
            <ProductDescription productName={props.product.name} productDescription={props.product.description} />
          </TabPanel>
          <TabPanel value={value} index={1}>
            <ProductParams productName={props.product.name} params={props.product.productParameter} />
          </TabPanel>
          <TabPanel value={value} index={2}>
            <ProductReviews
              productName={props.product.name}
              productId={props.product.id}
              reviewed={props.product.reviewed}
              saveReviewed={saveReviewed}
            />
          </TabPanel>
        </Grid>
      </Grid>
    </div>
  );
};

export default ProductInfoPanel;

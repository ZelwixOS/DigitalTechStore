import React, { useEffect } from 'react';
import Button from '@material-ui/core/Button';
import { createStyles, makeStyles, Theme } from '@material-ui/core/styles';
import {
  Card,
  CardContent,
  CardMedia,
  CircularProgress,
  Grid,
  MenuItem,
  Snackbar,
  TextField,
  Typography,
} from '@material-ui/core';
import { Alert } from '@mui/material';

import Category from 'src/Types/Category';
import { getAllCategories, getCategoryParamBlocks, getParameters, getProduct } from 'src/Requests/GetRequests';
import { updateProduct } from 'src/Requests/PutRequests';
import ParameterBlock from 'src/Types/ParameterBlock';
import ParameterCreateRequest from 'src/Types/ParameterCreateRequest';
import Parameter from 'src/Types/Parameter';

interface IRefresher {
  refresh: () => void;
}

const useStyles = makeStyles((theme: Theme) =>
  createStyles({
    spaces: {
      margin: theme.spacing(2),
    },
    input: {
      display: 'none',
    },
    pic: {
      height: 243,
      width: 432,
    },
    grid: {
      minWidth: 430,
    },
  }),
);

interface IEditProduct {
  id: string;
  refresher?: IRefresher;
  setOpen: React.Dispatch<React.SetStateAction<boolean>>;
}

const EditProduct: React.FC<IEditProduct> = props => {
  const classes = useStyles();

  const getData = async (isMounted: boolean) => {
    setLoading(true);
    const product = await getProduct(props.id);
    const initValues = await getParameters(props.id);
    const cats = await getAllCategories();

    if (isMounted) {
      setProductData({
        name: product.name,
        description: product.description,
        price: product.price,
        priceWithoutDiscount: product.priceWithoutDiscount as number,
        categoryId: product.category?.id as string,
        vendorCode: product.vendorCode,
      });

      setInitialPValues(initValues);
      setCategories(cats);
      setLoading(false);
      setPicUrl(`/products/${product.picURL}`);
    }
  };

  const parseInitValues = (paramBlocks: ParameterBlock[]) => {
    const hashInit = {} as { [key: string]: { [key: string]: Parameter } };
    const result = {} as { [key: string]: ParameterCreateRequest };

    for (const block of initialPValues) {
      const newVal = {} as { [key: string]: Parameter };
      for (const param of block.parameters) {
        newVal[param.parameterId as string] = param;
      }

      hashInit[block.id] = newVal;
    }

    for (const newBlock of paramBlocks) {
      const val = hashInit[newBlock.id];
      if (val) {
        for (const param of newBlock.parameters) {
          const p = val[param.id];
          if (p) {
            if (param.range) {
              result[param.id] = { parameterId: param.id, value: parseInt(p.value), productId: props.id };
            } else {
              result[param.id] = { parameterId: param.id, parameterValueId: p.parameterValueId, productId: props.id };
            }
          }
        }
      }
    }

    setParamVals(result);
  };

  const loadCategoryData = async (isMounted: boolean) => {
    setLoading(true);
    const params = await getCategoryParamBlocks(productData.categoryId);
    if (isMounted) {
      setParameters(params.includedBlocks);
      parseInitValues(params.includedBlocks);
      setLoading(false);
    }
  };

  const refreshData = () => {
    let isMounted = true;
    getData(isMounted);

    return () => {
      isMounted = false;
    };
  };

  const getCategoryData = () => {
    let isMounted = true;
    loadCategoryData(isMounted);

    return () => {
      isMounted = false;
    };
  };

  useEffect(() => {
    refreshData();
  }, []);

  const cloneData = () => ({
    name: productData.name,
    description: productData.description,
    price: productData.price,
    priceWithoutDiscount: productData.priceWithoutDiscount,
    categoryId: productData.categoryId,
    vendorCode: productData.vendorCode,
  });

  const [productData, setProductData] = React.useState({
    name: '',
    description: '',
    price: 0,
    priceWithoutDiscount: 0,
    categoryId: '',
    vendorCode: '',
  });

  const [paramVals, setParamVals] = React.useState<{ [key: string]: ParameterCreateRequest }>({});
  const [parameters, setParameters] = React.useState<ParameterBlock[]>([]);
  const [categories, setCategories] = React.useState<Category[]>([]);
  const [open, setOpen] = React.useState<boolean>(false);
  const [message, setMessage] = React.useState<string>('');
  const [loading, setLoading] = React.useState(true);
  const [step, setStep] = React.useState(1);
  const [pic, setPic] = React.useState<File>();
  const [picUrl, setPicUrl] = React.useState('');
  const [initialPValues, setInitialPValues] = React.useState<ParameterBlock[]>([]);

  const handleId = (id: string) => handleListParameterChange.bind(this, id);

  const handleRangeParameterChange = (event: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement> | undefined) => {
    if (event && paramVals) {
      const foundObject = paramVals[event.target.id] as ParameterCreateRequest;
      if (foundObject) {
        foundObject.value = parseInt(event.target.value);
        foundObject.parameterId = event.target.id;
      } else {
        paramVals[event.target.id] = {
          parameterId: event.target.id,
          value: parseInt(event.target.value),
          productId: props.id,
        };
      }

      setParamVals({ ...paramVals });
    }
  };

  const handleListParameterChange = (id: string, event: React.ChangeEvent<HTMLInputElement>) => {
    if (event && paramVals) {
      const foundObject = paramVals[id] as ParameterCreateRequest;
      if (foundObject) {
        foundObject.parameterValueId = event.target.value as string;
        foundObject.parameterId = id;
      } else {
        paramVals[id] = { parameterId: id, parameterValueId: event.target.value, productId: props.id };
      }

      setParamVals({ ...paramVals });
    }
  };

  const handleNameChange = (event: React.ChangeEvent<{ value: unknown }>) => {
    const data = cloneData();
    data.name = event.target.value as string;
    setProductData(data);
  };

  const handleVendorCodeChange = (event: React.ChangeEvent<{ value: unknown }>) => {
    const data = cloneData();
    data.vendorCode = event.target.value as string;
    setProductData(data);
  };

  const handleDescriptionChange = (event: React.ChangeEvent<{ value: unknown }>) => {
    const data = cloneData();
    data.description = event.target.value as string;
    setProductData(data);
  };

  const handlePriceChange = (event: React.ChangeEvent<{ value: unknown }>) => {
    const data = cloneData();
    const newPrice = event.target.value as number;
    if (newPrice > 0) {
      data.price = newPrice;
    }

    setProductData(data);
  };

  const handlePriceWithoutDiscountChange = (event: React.ChangeEvent<{ value: unknown }>) => {
    const data = cloneData();
    const newPrice = event.target.value as number;
    if (newPrice > 0) {
      data.priceWithoutDiscount = newPrice;
    }

    setProductData(data);
  };

  const handleCategoryChange = (event: React.ChangeEvent<{ value: unknown }>) => {
    const data = cloneData();
    data.categoryId = event.target.value as string;
    setProductData(data);
  };

  const handlePicChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    const file = event.target.files && event.target.files[0];
    if (file) {
      setPic(file);
      setPicUrl(URL.createObjectURL(file));
    }
  };

  const handleClose = (event?: React.SyntheticEvent, reason?: string) => {
    if (reason === 'clickaway') {
      return;
    }

    setOpen(false);
  };

  const onNext = async () => {
    if (productData.categoryId.length < 1) {
      setMessage('Выберите категорию!');
      setOpen(true);
    } else {
      getCategoryData();
      setStep(2);
    }
  };

  const onBack = async () => {
    setStep(1);
  };

  const onClick = async () => {
    if (productData.name.length < 2) {
      setMessage('Введите корректное название');
      setOpen(true);
    } else if (productData.description.length < 5) {
      setMessage('Введите корректное описание');
      setOpen(true);
    } else {
      const params = [];
      for (const name in paramVals) {
        params.push(paramVals[name]);
      }

      const res = await updateProduct(
        props.id,
        productData.name,
        productData.description,
        productData.price,
        productData.priceWithoutDiscount,
        productData.categoryId,
        productData.vendorCode,
        pic ?? null,
        params,
      );

      if (res && props.refresher) {
        props.refresher.refresh();
        props.setOpen(false);
      } else {
        setMessage('Не удалось выполнить запрос');
        setOpen(true);
      }
    }
  };

  return (
    <Grid container direction="column" justifyContent="center">
      <Snackbar
        anchorOrigin={{ vertical: 'top', horizontal: 'center' }}
        open={open}
        autoHideDuration={6000}
        onClose={handleClose}
      >
        <Alert onClose={handleClose} severity="warning">
          {message}
        </Alert>
      </Snackbar>
      {loading ? (
        <Grid container alignContent="stretch" justifyContent="center">
          <CircularProgress />
        </Grid>
      ) : (
        <React.Fragment>
          {step === 1 ? (
            <React.Fragment>
              <TextField
                id="productName"
                className={classes.spaces}
                value={productData.name}
                onChange={handleNameChange}
                label="Название"
                variant="outlined"
              />
              <TextField
                id="productDescription"
                className={classes.spaces}
                value={productData.description}
                onChange={handleDescriptionChange}
                label="Описание"
                multiline
                variant="outlined"
              />
              <TextField
                id="productPrice"
                variant="outlined"
                className={classes.spaces}
                type="number"
                label="Цена"
                value={productData.price}
                onChange={handlePriceChange}
              />
              <TextField
                id="productDiscontlessPrice"
                variant="outlined"
                className={classes.spaces}
                type="number"
                label="Цена без скидки"
                value={productData.priceWithoutDiscount}
                onChange={handlePriceWithoutDiscountChange}
              />
              <TextField
                id="productCode"
                variant="outlined"
                className={classes.spaces}
                type="number"
                label="Артикул"
                value={productData.vendorCode}
                onChange={handleVendorCodeChange}
              />
              <TextField
                id="productCategory"
                select
                label="Категория"
                className={classes.spaces}
                value={productData.categoryId}
                onChange={handleCategoryChange}
                variant="outlined"
              >
                {categories.map(cat => (
                  <MenuItem key={cat.id} value={cat.id}>
                    {cat.name}
                  </MenuItem>
                ))}
              </TextField>
              <input
                accept="image/*"
                className={classes.input}
                id="contained-button-file"
                type="file"
                onChange={handlePicChange}
              />
              <Card className={classes.spaces} variant="outlined">
                <CardMedia className={classes.pic} image={picUrl} title="Выбранное изображение" />
                <CardContent>
                  <Grid container justifyContent="center">
                    <label htmlFor="contained-button-file" className={classes.spaces}>
                      <Button variant="contained" color="primary" component="span">
                        Выбрать изображение
                      </Button>
                    </label>
                  </Grid>
                </CardContent>
              </Card>
            </React.Fragment>
          ) : (
            <Grid item xs={12} sm={9} container justifyContent="center" direction="column" className={classes.grid}>
              {parameters.map(pb => (
                <React.Fragment key={pb.id}>
                  <Typography>{pb.name}</Typography>
                  {pb.parameters.map(p => (
                    <React.Fragment key={p.id}>
                      {p.range ? (
                        <TextField
                          id={p.id}
                          className={classes.spaces}
                          value={paramVals && paramVals[p.id]?.value}
                          onChange={handleRangeParameterChange}
                          label={p.name}
                          type="number"
                          variant="outlined"
                        />
                      ) : (
                        <TextField
                          id={p.id}
                          className={classes.spaces}
                          value={paramVals && paramVals[p.id]?.parameterValueId}
                          onChange={handleId(p.id)}
                          label={p.name}
                          variant="outlined"
                          select
                        >
                          {p.parameterValues.map(pv => (
                            <MenuItem key={pv.id} value={pv.id}>
                              {pv.value}
                            </MenuItem>
                          ))}
                        </TextField>
                      )}
                    </React.Fragment>
                  ))}
                </React.Fragment>
              ))}
            </Grid>
          )}
          <Grid container justifyContent="flex-end">
            {step === 1 ? (
              <Button type="submit" className={classes.spaces} color="primary" variant="contained" onClick={onNext}>
                Далее
              </Button>
            ) : (
              <React.Fragment>
                <Grid xs={12} sm={6} item container justifyContent="flex-start">
                  <Button type="submit" className={classes.spaces} color="primary" variant="outlined" onClick={onBack}>
                    Назад
                  </Button>
                </Grid>
                <Grid xs={12} sm={6} item container justifyContent="flex-end">
                  <Button
                    type="submit"
                    className={classes.spaces}
                    color="primary"
                    variant="contained"
                    onClick={onClick}
                  >
                    Обновить
                  </Button>
                </Grid>
              </React.Fragment>
            )}
          </Grid>
        </React.Fragment>
      )}
    </Grid>
  );
};

export default EditProduct;

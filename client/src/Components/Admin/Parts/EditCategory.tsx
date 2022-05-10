import React, { Dispatch, SetStateAction, useEffect } from 'react';
import Button from '@material-ui/core/Button';
import { createStyles, makeStyles, Theme } from '@material-ui/core/styles';
import { CircularProgress, Grid, MenuItem, Snackbar, TextField, Typography } from '@material-ui/core';
import { Alert } from '@mui/material';

import { getCategoryById, getCategoryParamBlocks, getCommonCategories } from 'src/Requests/GetRequests';
import { updateCategory } from 'src/Requests/PutRequests';
import CommonCategory from 'src/Types/CommonCategory';
import ParameterBlock from 'src/Types/ParameterBlock';
import { IIdentifieable, TransferList } from 'src/Components/Admin/Parts/TransferList';
import { setCategoryParameterBlocks } from 'src/Requests/PostRequests';
import ParameterBlockCreateRequest from 'src/Types/ParameterBlockCreateRequest';

interface IRefresher {
  refresh: () => void;
}

const useStyles = makeStyles((theme: Theme) =>
  createStyles({
    spaces: {
      margin: theme.spacing(2),
    },
  }),
);

interface IEditCategory {
  id: string;
  setOpen: React.Dispatch<React.SetStateAction<boolean>>;
  refresher?: IRefresher;
}

const EditCategory: React.FC<IEditCategory> = props => {
  const classes = useStyles();

  const getData = async (isMounted: boolean) => {
    setLoading(true);
    const res = await getCategoryById(props.id);
    const comCats = await getCommonCategories();
    const paramBlocks = await getCategoryParamBlocks(props.id);
    if (isMounted) {
      setCategoryData({
        name: res.name,
        description: res.description as string,
        deliveryPrice: res.deliveryPrice as number,
        commonCategoryId: res.commonCategoryId as string,
      });
      setCommonCategories(comCats);
      setLeft(paramBlocks.includedBlocks);
      setRight(paramBlocks.excludedBlocks);
      setStartImportant(res.parameterBlocks);
      setLoading(false);
    }
  };

  const cloneData = () => ({
    name: categoryData.name,
    description: categoryData.description,
    deliveryPrice: categoryData.deliveryPrice,
    commonCategoryId: categoryData.commonCategoryId,
  });

  const getFinalData = (): ParameterBlockCreateRequest[] => {
    const parBlocks: ParameterBlockCreateRequest[] = [];
    for (const item of important) {
      parBlocks.push({ parameterBlockId: item.id, important: true });
    }

    for (const item of unimportant) {
      parBlocks.push({ parameterBlockId: item.id, important: true });
    }

    return parBlocks;
  };

  const refreshData = () => {
    let isMounted = true;
    getData(isMounted);

    return () => {
      isMounted = false;
    };
  };

  useEffect(() => {
    refreshData();
  }, []);

  const [categoryData, setCategoryData] = React.useState({
    name: '',
    description: '',
    deliveryPrice: 0,
    commonCategoryId: '',
  });
  const [commonCategories, setCommonCategories] = React.useState<CommonCategory[]>([]);
  const [open, setOpen] = React.useState<boolean>(false);
  const [message, setMessage] = React.useState<string>('');
  const [right, setRight] = React.useState<ParameterBlock[]>([]);
  const [startImportant, setStartImportant] = React.useState<ParameterBlock[]>([]);
  const [important, setImportant] = React.useState<ParameterBlock[]>([]);
  const [unimportant, setUnimportant] = React.useState<ParameterBlock[]>([]);
  const [left, setLeft] = React.useState<ParameterBlock[]>([]);
  const [loading, setLoading] = React.useState(true);
  const [step, setStep] = React.useState(1);

  const handleNameChange = (event: React.ChangeEvent<{ value: unknown }>) => {
    const data = cloneData();
    data.name = event.target.value as string;
    setCategoryData(data);
  };

  const handleDescriptionChange = (event: React.ChangeEvent<{ value: unknown }>) => {
    const data = cloneData();
    data.description = event.target.value as string;
    setCategoryData(data);
  };

  const handleDevPriceChange = (event: React.ChangeEvent<{ value: unknown }>) => {
    const data = cloneData();
    const newPrice = event.target.value as number;
    if (newPrice > 0) {
      data.deliveryPrice = newPrice;
    }

    setCategoryData(data);
  };

  const handleComCatChange = (event: React.ChangeEvent<{ value: unknown }>) => {
    const data = cloneData();
    data.commonCategoryId = event.target.value as string;
    setCategoryData(data);
  };

  const handleClose = (event?: React.SyntheticEvent, reason?: string) => {
    if (reason === 'clickaway') {
      return;
    }

    setOpen(false);
  };

  const onClick = async () => {
    if (categoryData.name.length < 2) {
      setMessage('Введите корректное название');
      setOpen(true);
    } else if (categoryData.description.length < 5) {
      setMessage('Введите корректное описание');
      setOpen(true);
    } else {
      const data = getFinalData();
      const res = await updateCategory(
        props.id,
        categoryData.name,
        categoryData.description,
        categoryData.deliveryPrice,
        categoryData.commonCategoryId,
      );
      const resBlocks = await setCategoryParameterBlocks(props.id, data);
      if (res && props.refresher && resBlocks) {
        props.refresher.refresh();
        props.setOpen(false);
      } else {
        setMessage('Не удалось выполнить запрос');
        setOpen(true);
      }
    }
  };

  const onNext = async () => {
    const imp: ParameterBlock[] = [];
    const unimp: ParameterBlock[] = [];
    for (const item of left) {
      if (startImportant.find(p => p.id === item.id)) {
        imp.push(item);
      } else {
        unimp.push(item);
      }
    }

    setImportant(imp);
    setUnimportant(unimp);
    setStep(2);
  };

  const onBack = async () => {
    setStep(1);
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
          <TextField
            id="categoryName"
            className={classes.spaces}
            value={categoryData.name}
            onChange={handleNameChange}
            label="Название"
            variant="outlined"
          />
          <TextField
            id="categoryDescription"
            className={classes.spaces}
            value={categoryData.description}
            onChange={handleDescriptionChange}
            label="Описание"
            variant="outlined"
          />
          <TextField
            id="categoryDeliveryPrice"
            variant="outlined"
            className={classes.spaces}
            type="number"
            label="Цена доставки"
            value={categoryData.deliveryPrice}
            onChange={handleDevPriceChange}
          />
          <TextField
            id="commonCategory"
            select
            label="Обобщённая категория"
            className={classes.spaces}
            value={categoryData.commonCategoryId}
            onChange={handleComCatChange}
            variant="outlined"
          >
            {commonCategories.map(comCat => (
              <MenuItem key={comCat.id} value={comCat.id}>
                {comCat.name}
              </MenuItem>
            ))}
          </TextField>
          {step === 1 ? (
            <React.Fragment>
              <Grid container item xs={12} justifyContent="space-evenly" direction="row">
                <Grid item xs={12} sm={6}>
                  <Typography align="center" variant="h6">
                    Включены
                  </Typography>
                </Grid>
                <Grid item xs={12} sm={6}>
                  <Typography align="center" variant="h6">
                    Все
                  </Typography>
                </Grid>
              </Grid>
              <TransferList
                left={left}
                right={right}
                setLeft={setLeft as Dispatch<SetStateAction<IIdentifieable<string>[]>>}
                setRight={setRight as Dispatch<SetStateAction<IIdentifieable<string>[]>>}
              />
            </React.Fragment>
          ) : (
            <React.Fragment>
              <Grid container item xs={12} justifyContent="space-evenly" direction="row">
                <Grid item xs={12} sm={6}>
                  <Typography align="center" variant="h6">
                    Важные
                  </Typography>
                </Grid>
                <Grid item xs={12} sm={6}>
                  <Typography align="center" variant="h6">
                    Не важные
                  </Typography>
                </Grid>
              </Grid>
              <TransferList
                left={important}
                right={unimportant}
                setLeft={setImportant as Dispatch<SetStateAction<IIdentifieable<string>[]>>}
                setRight={setUnimportant as Dispatch<SetStateAction<IIdentifieable<string>[]>>}
              />
            </React.Fragment>
          )}
          <Grid container direction="row" justifyContent="flex-end">
            {step === 1 ? (
              <Button type="submit" className={classes.spaces} color="primary" variant="contained" onClick={onNext}>
                Далее
              </Button>
            ) : (
              <React.Fragment>
                <Grid xs={12} sm={6} item>
                  <Button type="submit" className={classes.spaces} color="primary" variant="outlined" onClick={onBack}>
                    Назад
                  </Button>
                </Grid>
                <Grid>
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

export default EditCategory;

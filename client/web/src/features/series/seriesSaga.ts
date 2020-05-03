import { put, takeLatest, call, all } from 'redux-saga/effects'

import * as actionTypes from './actionTypes'
import seriesService from './seriesService'
import { UpdateSeriesAction } from './types'

export function* fetchSeries() {
  const { data: series } = yield call([seriesService, 'getAll'])
  yield put({
    type: actionTypes.SET_ALL_SERIES,
    series,
  })
}

export function* updateSeries(action: UpdateSeriesAction) {
  const { series } = action
  try
  {
    yield call([seriesService, 'update'], series.id, series)
    yield call(fetchSeries)
  } catch (error)
  {
    console.log(error);
  }
}

export function* actionWatcher() {
  yield takeLatest(actionTypes.FETCH_ALL_SERIES, fetchSeries)
  yield takeLatest(actionTypes.UPDATE_SERIES, updateSeries)
}

export default function* seriesSaga() {
  yield all([actionWatcher()])
}

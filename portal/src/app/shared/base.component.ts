export abstract class BaseComponent{
    private _isProgressVisible: boolean;

    constructor(){
        this.hideProgress();
    }

    get isProgressVisible(): boolean{
        return this._isProgressVisible;
    }

    showProgress(): void{
        this._isProgressVisible = true;
    };

    hideProgress(): void{
        this._isProgressVisible = false;
    };
}
import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { PaymentsService } from '../../services/payments-service';
import { BtcDepositForm } from '../../viewmodels/payment/BtcDepositForm';
import { UserDataForm } from '../../viewmodels/user/UserDataForm';
import { MoneyTransferForm } from '../../viewmodels/payment/MoneyTransferForm';
import { NotificationsService } from '../notifications/notifications.service';
import { UserService } from '../../services/user-service';
import { PaymentRepository } from '../../http-access/payment-repository';


@Component({
  selector: 'app-cashier',
  templateUrl: './cashier.component.html',
  styleUrls: ['./cashier.component.scss']
})

export class CashierComponent implements OnInit {

  public DisplayCryptoForm: boolean = false;
  public USDAmount: number;
  public BonusCode: string;
  public DepoCode: string;
  public AvailableAmount: number;
  public CashoutAmount: number;
  public InPlay: number;
  public Total: number;
  public IsDepositMade: boolean = false;
  public AmountBtc: string;
  public Address: string;
  public QrCode: string;
  public ValidBuyAmount: boolean = true;
  public ValidCashoutAmount: boolean = true;
  public ValidAddress : boolean = true;
  public ValidDepoCode: boolean = true;
  public UserBTCWalletAddress: string;
  public UserId: string;
  public Usermail: string;
  public UserMoneyForms: MoneyTransferForm[];
  public ShowTransaction: boolean = false;

  public BuyTokensBtcVisible: boolean = false;
  public BuyTokensVisaVisible: boolean = false;
  public DepoCodeDepositVisible: boolean = false;

  public CashoutBtcVisible: boolean = false;
  public CashoutVisaVisible: boolean = false;

  @ViewChild('DoDepositSection') InputDepositSectionElement: ElementRef;

  constructor(
    private paymentService: PaymentsService,
    private paymentRepository: PaymentRepository,
    private userService: UserService,
    private notificationService: NotificationsService) { }

  ngOnInit() {
    
    this.userService.UserData.subscribe(
      (data: UserDataForm) => {
        if (data != null)
        {
          this.AvailableAmount = data.balance;
          this.InPlay = data.inPlay;
          this.Total = data.total;
          this.UserId = data.id;
          this.Usermail = data.usermail;
          this.GetMoneyTransfers();
        }
      },
      (err) => {
        // console.log(err);
      });
      this.userService.FetchUserData(); 
  }

  public CreateBtcDeposit() 
  {
    let validDeposit = this.ValidateDepositInput();
    let isNumber = !Number.isNaN(Number(this.USDAmount));
    let isInRange = Number(this.USDAmount) >= 10;

    if(isNumber && isInRange)
    {
      this.paymentService.CreateBtcDeposit(this.USDAmount, this.BonusCode, this.Usermail, this.UserId).subscribe(
        (data: BtcDepositForm) => {
          this.IsDepositMade = true;
          this.AmountBtc = data.amount;
          this.Address = data.address;
          this.QrCode = data.qrcode_url;
          this.paymentService._currentTransactionUpdated.next(data);
        },
        (err) => {
          console.log(err);
        });
    }
    else
    {
      this.ValidBuyAmount = false;
    }
  }

  public CreateBtcWithdraw()
  {
    let validCashout = this.ValidateCashout();
    if (this.CashoutAmount > this.AvailableAmount)
    {
      this.notificationService.NotifyInfo('You dont have enought money.', 3500);
    }
    else if(validCashout)
    {
      this.paymentService.CreateBtcWithdraw(this.CashoutAmount, this.UserBTCWalletAddress, this.Usermail, this.UserId).subscribe(
        (data: string) => {
          this.CashoutAmount = null;
          this.UserBTCWalletAddress = "";
          this.notificationService.NotifySuccess('Your cashout request is pending.', 3500);
          this.AvailableAmount -= this.CashoutAmount;
        },
        (err) => {
          this.notificationService.NotifyError('Error during cashout.', 3500);
        });
    }
  }

  public UseDepoCode()
  {
    this.ValidDepoCode = this.ValidateDepoCode(this.DepoCode);

    this.paymentRepository.UseDepoCode(this.UserId, this.DepoCode).subscribe(
      (data: string) => {
        this.notificationService.NotifySuccess('Code was used.', 3500);
      },
      (err) => {
        this.notificationService.NotifyError('Error during cashout.', 3500);
    });

    this.DepoCodeDepositVisible = false;
  }

  public ToggleBuyWithBtcForm()
  {
    this.BuyTokensVisaVisible = false;
    this.CashoutBtcVisible = false;
    this.CashoutVisaVisible = false;
    this.DepoCodeDepositVisible = false;
    this.BuyTokensBtcVisible = !this.BuyTokensBtcVisible;
    if (this.BuyTokensBtcVisible)
    {
      this.ScrollToPaymentView();
    }
  }

  public ToggleDepoCodeInput()
  {
    this.BuyTokensBtcVisible = false;
    this.BuyTokensVisaVisible = false;
    this.CashoutBtcVisible = false;
    this.CashoutVisaVisible = false;
    this.DepoCodeDepositVisible = !this.DepoCodeDepositVisible;
    if (this.DepoCodeDepositVisible)
    {
      this.ScrollToPaymentView();
    }
  }

  public ToggleBuyVisaInput()
  {
    this.BuyTokensBtcVisible = false;
    this.CashoutBtcVisible = false;
    this.CashoutVisaVisible = false;
    this.DepoCodeDepositVisible = false;
    this.BuyTokensVisaVisible = !this.BuyTokensVisaVisible;
    if (this.BuyTokensVisaVisible)
    {
      this.ScrollToPaymentView();
    }
  }

  public ToggleCashoutBtcInput()
  {
    this.BuyTokensBtcVisible = false;
    this.BuyTokensVisaVisible = false;
    this.CashoutVisaVisible = false;
    this.DepoCodeDepositVisible = false;
    this.CashoutBtcVisible = !this.CashoutBtcVisible;
    if (this.CashoutBtcVisible)
    {
      this.ScrollToPaymentView();
    }
  }

  public ToggleCashoutVisaInput()
  {
    this.BuyTokensBtcVisible = false;
    this.BuyTokensVisaVisible = false;
    this.CashoutBtcVisible = false;
    this.DepoCodeDepositVisible = false;
    this.CashoutVisaVisible = !this.CashoutVisaVisible;
    if (this.CashoutVisaVisible)
    {
      this.ScrollToPaymentView();
    }
  }

  public CancellPaymentForm()
  {
    this.IsDepositMade = false;
  }

  public GetMoneyTransfers()
  {
    this.paymentService.GetUsersTransfers(this.UserId).subscribe(
      (data: MoneyTransferForm[]) => {
        this.UserMoneyForms = data;
        this.ShowTransaction = true
      },
      (err) => {
        this.ShowTransaction = false;
      });
  }

  public ValidateDepositInput()
  {
    if (this.USDAmount == null || this.USDAmount === null)
    {
      this.ValidBuyAmount = false;
      return this.ValidBuyAmount;
    }
    let isNumber = !Number.isNaN(Number(this.USDAmount));
    let isPositive = Number(this.USDAmount) > 0;


    if(isNumber && isPositive || this.USDAmount.toString() == "") 
    {
      this.ValidBuyAmount = true;
    }
    else 
    {
      //red border on input
      this.ValidBuyAmount = false;
    }
    return this.ValidBuyAmount;
  }

  public ValidateCashout()
  {
    let isValid = true;
    if (this.UserBTCWalletAddress == null || this.UserBTCWalletAddress === null)
    {
      this.ValidAddress = false;
      isValid = false;
    }
    else if (this.UserBTCWalletAddress.length != 34)
    {
      this.ValidAddress = false;
      isValid = false;
    }

    if (this.CashoutAmount == null || this.CashoutAmount === null)
    {
      this.ValidCashoutAmount = false;
      isValid = false;
    }
    else
    {
      if (this.CashoutAmount.toString() == "")
      {
        this.ValidCashoutAmount = false;
        isValid = false;
      }
      let isNumber = !Number.isNaN(Number(this.CashoutAmount));
      let isInRange = Number(this.CashoutAmount) >= 20;
  
      if (isNumber && isInRange)
      {
        this.ValidCashoutAmount = true;
        if (isValid)
        {
          isValid = true;
        }
      }
      else
      {
        this.ValidCashoutAmount = false;
        isValid = false;
      }
    }

    return isValid;
  }
  
  public ValidateDepoCode(code: string): boolean 
  {
    return code.length == 7;
  }

  public CopyToClipboard(val: string) 
  {
    let selBox = document.createElement('textarea');
      selBox.style.position = 'fixed';
      selBox.style.left = '0';
      selBox.style.top = '0';
      selBox.style.opacity = '0';
      selBox.value = val;
      document.body.appendChild(selBox);
      selBox.focus();
      selBox.select();
      document.execCommand('copy');
      document.body.removeChild(selBox);
  }

  private ScrollToPaymentView()
  {
    let el = this.InputDepositSectionElement.nativeElement;
    el.scrollIntoView();
  }


}


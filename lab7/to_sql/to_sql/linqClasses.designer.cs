﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace to_sql
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	using System.ComponentModel;
	using System;
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="supermarket")]
	public partial class linqClassesDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Определения метода расширяемости
    partial void OnCreated();
    partial void InserttbTransaction(tbTransaction instance);
    partial void UpdatetbTransaction(tbTransaction instance);
    partial void DeletetbTransaction(tbTransaction instance);
    partial void InserttbBonusCard(tbBonusCard instance);
    partial void UpdatetbBonusCard(tbBonusCard instance);
    partial void DeletetbBonusCard(tbBonusCard instance);
    partial void InserttbClient(tbClient instance);
    partial void UpdatetbClient(tbClient instance);
    partial void DeletetbClient(tbClient instance);
    partial void InserttbProduct(tbProduct instance);
    partial void UpdatetbProduct(tbProduct instance);
    partial void DeletetbProduct(tbProduct instance);
    #endregion
		
		public linqClassesDataContext() : 
				base(global::to_sql.Properties.Settings.Default.supermarketConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public linqClassesDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public linqClassesDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public linqClassesDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public linqClassesDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<tbTransaction> tbTransaction
		{
			get
			{
				return this.GetTable<tbTransaction>();
			}
		}
		
		public System.Data.Linq.Table<tbBonusCard> tbBonusCard
		{
			get
			{
				return this.GetTable<tbBonusCard>();
			}
		}
		
		public System.Data.Linq.Table<tbClient> tbClient
		{
			get
			{
				return this.GetTable<tbClient>();
			}
		}
		
		public System.Data.Linq.Table<tbOrder> tbOrder
		{
			get
			{
				return this.GetTable<tbOrder>();
			}
		}
		
		public System.Data.Linq.Table<tbProduct> tbProduct
		{
			get
			{
				return this.GetTable<tbProduct>();
			}
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.GetClient")]
		public ISingleResult<GetClientResult> GetClient()
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
			return ((ISingleResult<GetClientResult>)(result.ReturnValue));
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.tbTransaction")]
	public partial class tbTransaction : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _id;
		
		private int _idClient;
		
		private System.DateTime _date;
		
		private System.TimeSpan _time;
		
		private int _paymentAmount;
		
		private string _type;
		
		private int _cashboxNumber;
		
		private EntitySet<tbBonusCard> _tbBonusCard;
		
		private EntityRef<tbClient> _tbClient;
		
    #region Определения метода расширяемости
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnidChanging(int value);
    partial void OnidChanged();
    partial void OnidClientChanging(int value);
    partial void OnidClientChanged();
    partial void OndateChanging(System.DateTime value);
    partial void OndateChanged();
    partial void OntimeChanging(System.TimeSpan value);
    partial void OntimeChanged();
    partial void OnpaymentAmountChanging(int value);
    partial void OnpaymentAmountChanged();
    partial void OntypeChanging(string value);
    partial void OntypeChanged();
    partial void OncashboxNumberChanging(int value);
    partial void OncashboxNumberChanged();
    #endregion
		
		public tbTransaction()
		{
			this._tbBonusCard = new EntitySet<tbBonusCard>(new Action<tbBonusCard>(this.attach_tbBonusCard), new Action<tbBonusCard>(this.detach_tbBonusCard));
			this._tbClient = default(EntityRef<tbClient>);
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_id", DbType="Int NOT NULL", IsPrimaryKey=true)]
		public int id
		{
			get
			{
				return this._id;
			}
			set
			{
				if ((this._id != value))
				{
					this.OnidChanging(value);
					this.SendPropertyChanging();
					this._id = value;
					this.SendPropertyChanged("id");
					this.OnidChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_idClient", DbType="Int NOT NULL")]
		public int idClient
		{
			get
			{
				return this._idClient;
			}
			set
			{
				if ((this._idClient != value))
				{
					if (this._tbClient.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnidClientChanging(value);
					this.SendPropertyChanging();
					this._idClient = value;
					this.SendPropertyChanged("idClient");
					this.OnidClientChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_date", DbType="Date NOT NULL")]
		public System.DateTime date
		{
			get
			{
				return this._date;
			}
			set
			{
				if ((this._date != value))
				{
					this.OndateChanging(value);
					this.SendPropertyChanging();
					this._date = value;
					this.SendPropertyChanged("date");
					this.OndateChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_time", DbType="Time NOT NULL")]
		public System.TimeSpan time
		{
			get
			{
				return this._time;
			}
			set
			{
				if ((this._time != value))
				{
					this.OntimeChanging(value);
					this.SendPropertyChanging();
					this._time = value;
					this.SendPropertyChanged("time");
					this.OntimeChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_paymentAmount", DbType="Int NOT NULL")]
		public int paymentAmount
		{
			get
			{
				return this._paymentAmount;
			}
			set
			{
				if ((this._paymentAmount != value))
				{
					this.OnpaymentAmountChanging(value);
					this.SendPropertyChanging();
					this._paymentAmount = value;
					this.SendPropertyChanged("paymentAmount");
					this.OnpaymentAmountChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_type", DbType="NVarChar(200)")]
		public string type
		{
			get
			{
				return this._type;
			}
			set
			{
				if ((this._type != value))
				{
					this.OntypeChanging(value);
					this.SendPropertyChanging();
					this._type = value;
					this.SendPropertyChanged("type");
					this.OntypeChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_cashboxNumber", DbType="Int NOT NULL")]
		public int cashboxNumber
		{
			get
			{
				return this._cashboxNumber;
			}
			set
			{
				if ((this._cashboxNumber != value))
				{
					this.OncashboxNumberChanging(value);
					this.SendPropertyChanging();
					this._cashboxNumber = value;
					this.SendPropertyChanged("cashboxNumber");
					this.OncashboxNumberChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="tbTransaction_tbBonusCard", Storage="_tbBonusCard", ThisKey="id", OtherKey="idTransaction")]
		public EntitySet<tbBonusCard> tbBonusCard
		{
			get
			{
				return this._tbBonusCard;
			}
			set
			{
				this._tbBonusCard.Assign(value);
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="tbClient_tbTransaction", Storage="_tbClient", ThisKey="idClient", OtherKey="id", IsForeignKey=true, DeleteOnNull=true, DeleteRule="CASCADE")]
		public tbClient tbClient
		{
			get
			{
				return this._tbClient.Entity;
			}
			set
			{
				tbClient previousValue = this._tbClient.Entity;
				if (((previousValue != value) 
							|| (this._tbClient.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._tbClient.Entity = null;
						previousValue.tbTransaction.Remove(this);
					}
					this._tbClient.Entity = value;
					if ((value != null))
					{
						value.tbTransaction.Add(this);
						this._idClient = value.id;
					}
					else
					{
						this._idClient = default(int);
					}
					this.SendPropertyChanged("tbClient");
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		
		private void attach_tbBonusCard(tbBonusCard entity)
		{
			this.SendPropertyChanging();
			entity.tbTransaction = this;
		}
		
		private void detach_tbBonusCard(tbBonusCard entity)
		{
			this.SendPropertyChanging();
			entity.tbTransaction = null;
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.tbBonusCard")]
	public partial class tbBonusCard : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _id;
		
		private int _idTransaction;
		
		private int _idClient;
		
		private long _cardNumber;
		
		private System.DateTime _issueDate;
		
		private System.Nullable<int> _bonusCount;
		
		private EntityRef<tbTransaction> _tbTransaction;
		
    #region Определения метода расширяемости
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnidChanging(int value);
    partial void OnidChanged();
    partial void OnidTransactionChanging(int value);
    partial void OnidTransactionChanged();
    partial void OnidClientChanging(int value);
    partial void OnidClientChanged();
    partial void OncardNumberChanging(long value);
    partial void OncardNumberChanged();
    partial void OnissueDateChanging(System.DateTime value);
    partial void OnissueDateChanged();
    partial void OnbonusCountChanging(System.Nullable<int> value);
    partial void OnbonusCountChanged();
    #endregion
		
		public tbBonusCard()
		{
			this._tbTransaction = default(EntityRef<tbTransaction>);
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_id", DbType="Int NOT NULL", IsPrimaryKey=true)]
		public int id
		{
			get
			{
				return this._id;
			}
			set
			{
				if ((this._id != value))
				{
					this.OnidChanging(value);
					this.SendPropertyChanging();
					this._id = value;
					this.SendPropertyChanged("id");
					this.OnidChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_idTransaction", DbType="Int NOT NULL")]
		public int idTransaction
		{
			get
			{
				return this._idTransaction;
			}
			set
			{
				if ((this._idTransaction != value))
				{
					if (this._tbTransaction.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnidTransactionChanging(value);
					this.SendPropertyChanging();
					this._idTransaction = value;
					this.SendPropertyChanged("idTransaction");
					this.OnidTransactionChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_idClient", DbType="Int NOT NULL")]
		public int idClient
		{
			get
			{
				return this._idClient;
			}
			set
			{
				if ((this._idClient != value))
				{
					this.OnidClientChanging(value);
					this.SendPropertyChanging();
					this._idClient = value;
					this.SendPropertyChanged("idClient");
					this.OnidClientChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_cardNumber", DbType="BigInt NOT NULL")]
		public long cardNumber
		{
			get
			{
				return this._cardNumber;
			}
			set
			{
				if ((this._cardNumber != value))
				{
					this.OncardNumberChanging(value);
					this.SendPropertyChanging();
					this._cardNumber = value;
					this.SendPropertyChanged("cardNumber");
					this.OncardNumberChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_issueDate", DbType="Date NOT NULL")]
		public System.DateTime issueDate
		{
			get
			{
				return this._issueDate;
			}
			set
			{
				if ((this._issueDate != value))
				{
					this.OnissueDateChanging(value);
					this.SendPropertyChanging();
					this._issueDate = value;
					this.SendPropertyChanged("issueDate");
					this.OnissueDateChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_bonusCount", DbType="Int")]
		public System.Nullable<int> bonusCount
		{
			get
			{
				return this._bonusCount;
			}
			set
			{
				if ((this._bonusCount != value))
				{
					this.OnbonusCountChanging(value);
					this.SendPropertyChanging();
					this._bonusCount = value;
					this.SendPropertyChanged("bonusCount");
					this.OnbonusCountChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="tbTransaction_tbBonusCard", Storage="_tbTransaction", ThisKey="idTransaction", OtherKey="id", IsForeignKey=true, DeleteOnNull=true, DeleteRule="CASCADE")]
		public tbTransaction tbTransaction
		{
			get
			{
				return this._tbTransaction.Entity;
			}
			set
			{
				tbTransaction previousValue = this._tbTransaction.Entity;
				if (((previousValue != value) 
							|| (this._tbTransaction.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._tbTransaction.Entity = null;
						previousValue.tbBonusCard.Remove(this);
					}
					this._tbTransaction.Entity = value;
					if ((value != null))
					{
						value.tbBonusCard.Add(this);
						this._idTransaction = value.id;
					}
					else
					{
						this._idTransaction = default(int);
					}
					this.SendPropertyChanged("tbTransaction");
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.tbClient")]
	public partial class tbClient : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _id;
		
		private string _clientName;
		
		private string _clientTelephoneNumber;
		
		private string _clientEmail;
		
		private EntitySet<tbTransaction> _tbTransaction;
		
    #region Определения метода расширяемости
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnidChanging(int value);
    partial void OnidChanged();
    partial void OnclientNameChanging(string value);
    partial void OnclientNameChanged();
    partial void OnclientTelephoneNumberChanging(string value);
    partial void OnclientTelephoneNumberChanged();
    partial void OnclientEmailChanging(string value);
    partial void OnclientEmailChanged();
    #endregion
		
		public tbClient()
		{
			this._tbTransaction = new EntitySet<tbTransaction>(new Action<tbTransaction>(this.attach_tbTransaction), new Action<tbTransaction>(this.detach_tbTransaction));
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_id", DbType="Int NOT NULL", IsPrimaryKey=true)]
		public int id
		{
			get
			{
				return this._id;
			}
			set
			{
				if ((this._id != value))
				{
					this.OnidChanging(value);
					this.SendPropertyChanging();
					this._id = value;
					this.SendPropertyChanged("id");
					this.OnidChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_clientName", DbType="NVarChar(200)")]
		public string clientName
		{
			get
			{
				return this._clientName;
			}
			set
			{
				if ((this._clientName != value))
				{
					this.OnclientNameChanging(value);
					this.SendPropertyChanging();
					this._clientName = value;
					this.SendPropertyChanged("clientName");
					this.OnclientNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_clientTelephoneNumber", DbType="NVarChar(200)")]
		public string clientTelephoneNumber
		{
			get
			{
				return this._clientTelephoneNumber;
			}
			set
			{
				if ((this._clientTelephoneNumber != value))
				{
					this.OnclientTelephoneNumberChanging(value);
					this.SendPropertyChanging();
					this._clientTelephoneNumber = value;
					this.SendPropertyChanged("clientTelephoneNumber");
					this.OnclientTelephoneNumberChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_clientEmail", DbType="NVarChar(200)")]
		public string clientEmail
		{
			get
			{
				return this._clientEmail;
			}
			set
			{
				if ((this._clientEmail != value))
				{
					this.OnclientEmailChanging(value);
					this.SendPropertyChanging();
					this._clientEmail = value;
					this.SendPropertyChanged("clientEmail");
					this.OnclientEmailChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="tbClient_tbTransaction", Storage="_tbTransaction", ThisKey="id", OtherKey="idClient")]
		public EntitySet<tbTransaction> tbTransaction
		{
			get
			{
				return this._tbTransaction;
			}
			set
			{
				this._tbTransaction.Assign(value);
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		
		private void attach_tbTransaction(tbTransaction entity)
		{
			this.SendPropertyChanging();
			entity.tbClient = this;
		}
		
		private void detach_tbTransaction(tbTransaction entity)
		{
			this.SendPropertyChanging();
			entity.tbClient = null;
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.tbOrder")]
	public partial class tbOrder
	{
		
		private int _idTransaction;
		
		private int _idProduct;
		
		public tbOrder()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_idTransaction", DbType="Int NOT NULL")]
		public int idTransaction
		{
			get
			{
				return this._idTransaction;
			}
			set
			{
				if ((this._idTransaction != value))
				{
					this._idTransaction = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_idProduct", DbType="Int NOT NULL")]
		public int idProduct
		{
			get
			{
				return this._idProduct;
			}
			set
			{
				if ((this._idProduct != value))
				{
					this._idProduct = value;
				}
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.tbProduct")]
	public partial class tbProduct : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _id;
		
		private string _productName;
		
		private int _productPrice;
		
		private System.Nullable<byte> _productInStock;
		
    #region Определения метода расширяемости
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnidChanging(int value);
    partial void OnidChanged();
    partial void OnproductNameChanging(string value);
    partial void OnproductNameChanged();
    partial void OnproductPriceChanging(int value);
    partial void OnproductPriceChanged();
    partial void OnproductInStockChanging(System.Nullable<byte> value);
    partial void OnproductInStockChanged();
    #endregion
		
		public tbProduct()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_id", DbType="Int NOT NULL", IsPrimaryKey=true)]
		public int id
		{
			get
			{
				return this._id;
			}
			set
			{
				if ((this._id != value))
				{
					this.OnidChanging(value);
					this.SendPropertyChanging();
					this._id = value;
					this.SendPropertyChanged("id");
					this.OnidChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_productName", DbType="NVarChar(200)")]
		public string productName
		{
			get
			{
				return this._productName;
			}
			set
			{
				if ((this._productName != value))
				{
					this.OnproductNameChanging(value);
					this.SendPropertyChanging();
					this._productName = value;
					this.SendPropertyChanged("productName");
					this.OnproductNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_productPrice", DbType="Int NOT NULL")]
		public int productPrice
		{
			get
			{
				return this._productPrice;
			}
			set
			{
				if ((this._productPrice != value))
				{
					this.OnproductPriceChanging(value);
					this.SendPropertyChanging();
					this._productPrice = value;
					this.SendPropertyChanged("productPrice");
					this.OnproductPriceChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_productInStock", DbType="TinyInt")]
		public System.Nullable<byte> productInStock
		{
			get
			{
				return this._productInStock;
			}
			set
			{
				if ((this._productInStock != value))
				{
					this.OnproductInStockChanging(value);
					this.SendPropertyChanging();
					this._productInStock = value;
					this.SendPropertyChanged("productInStock");
					this.OnproductInStockChanged();
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	public partial class GetClientResult
	{
		
		private int _id;
		
		private string _clientName;
		
		public GetClientResult()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_id", DbType="Int NOT NULL")]
		public int id
		{
			get
			{
				return this._id;
			}
			set
			{
				if ((this._id != value))
				{
					this._id = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_clientName", DbType="NVarChar(200)")]
		public string clientName
		{
			get
			{
				return this._clientName;
			}
			set
			{
				if ((this._clientName != value))
				{
					this._clientName = value;
				}
			}
		}
	}
}
#pragma warning restore 1591

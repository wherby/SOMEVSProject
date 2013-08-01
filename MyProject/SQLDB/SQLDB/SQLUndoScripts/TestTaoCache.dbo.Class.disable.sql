IF EXISTS (SELECT * FROM sys.change_tracking_tables WHERE object_id = OBJECT_ID(N'[dbo].[Class]')) 
   ALTER TABLE [dbo].[Class] 
   DISABLE  CHANGE_TRACKING
GO
